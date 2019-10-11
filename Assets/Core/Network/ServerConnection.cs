using System.Collections;
using System;
using Ionic.Zlib;
using UnityEngine;
using System.IO;
using Network.Packet;
using Network.Packet.Clientbound.Play;
using Network.Packet.Serverbound.Login;
using Network.Packet.Serverbound.Play;
using Clientbound = Network.Packet.Clientbound;
using Serverbound = Network.Packet.Serverbound;

namespace Network {
	public class ServerConnection : MonoBehaviour {
		private const int PARTIAL_DECOMPRESS_MIN_SIZE = 8096;
		private const int PARTIAL_DECOMPRESS_BUFFER_SIZE = 2048;
		public String ServerUrl = "localhost:42124";
		public PacketHandler PacketHandler {
			get {
				return _packetHandler;
			}
		}
		private PacketHandler _packetHandler;
		private WebSocket _socket;
		private bool _connected = false;
		private int _threshold = -1;

		IEnumerator Start() {
			_packetHandler = new PacketHandler();
			_socket = new WebSocket(new Uri("ws://" + GameSession.ServerUrl + "/game"));
			ReadWriteBuffer buffer = new ReadWriteBuffer(16777216);
			bool handshakeSent = false;

			PacketHandler.LoginPacketReceivedEvent += OnSetCompressionPacket;
			PacketHandler.PlayPacketReceivedEvent += OnPlayerAbilitiesPacket;
			PacketHandler.PlayPacketReceivedEvent += OnKeepAlivePacket;

			int size = -1;
            float lastWSKeepAlive = Time.time;
			yield return StartCoroutine(_socket.Connect());
			while (true) {
                // Send "keep alive" to bridge/webcraft server
                if (Time.time - lastWSKeepAlive > 0.5f) {
                    lastWSKeepAlive = Time.time;
                    _socket.Send(new byte[0]);
                }

                byte[] inByte = _socket.Recv();
				
				if (inByte != null && inByte.Length > 0) {
					buffer.Write(inByte);
				}

				while (buffer.Count > 4 && size == -1) {
					size = buffer.ReadVarInt();
				}


				if (size != -1 && size <= buffer.Count) {
					MemoryStream packet = new MemoryStream(buffer.Read(size));
					size = -1;
					StartCoroutine(ReadPacket(packet));
				}

				if (!handshakeSent) {
					new HandshakePacket(340, "", 0, HandshakePacket.States.Login)
						.Send(this);

					SendPacket(new MemoryStream()
						.WriteString(GameSession.Login), 0x00, false);
					handshakeSent = true;
				}
				
				if (_socket.error != null) {
					Debug.LogError ("Error: " + _socket.error);
					break;
				}

				if (buffer.Count <= 4) {
                    yield return new PriorityYieldInstruction(PriorityYieldInstruction.Priority.High);
                }
			}
			_socket.Close();
		}

		public void OnSetCompressionPacket(IPacket packet) {
			var scp = packet as Packet.Clientbound.Login.SetCompressionPacket;
			if (scp != null) {
				_threshold = scp.Threshold;
			}
		}

		public void OnPlayerAbilitiesPacket(IPacket packet) {
			var pap = packet as PlayerAbilitiesPacket;
			if (pap != null) {
				if (!_connected) {
					_connected = true;

					SendPacket(new MemoryStream()
						.WriteString("en_US")
						.WriteUByte(GameSettings.RenderDistance)
						.WriteVarInt(0)
						.WriteBool(true)
						.WriteUByte(0)
						.WriteVarInt(0), 0x04, false);
				}
			}
		}

		public void OnKeepAlivePacket(IPacket packet) {
			
			var kap = packet as Clientbound.Play.KeepAlivePacket;
			if (kap != null) {
				new Serverbound.Play.KeepAlivePacket(kap.KeepAliveID).Send(this);
			}
		}

		public void SendPacket(MemoryStream data, int id, bool connectionNeed = true) {
			if (connectionNeed && !_connected) {
				return;
			}
			if (_threshold > 0) {
				MemoryStream newData = new MemoryStream();
				newData.WriteVarInt(id);
				newData.Write(data);
				int uncompressedSize = (int) newData.Length;
				if (uncompressedSize >= _threshold) {
					newData = ZlibUtils.Compress(newData, Ionic.Zlib.CompressionLevel.BestSpeed);
				} else {
					uncompressedSize = 0;
				}
				int size = (int) newData.Length + VarIntSize(uncompressedSize);
				data.SetLength(0);
				data.WriteVarInt(size);
				data.WriteVarInt(uncompressedSize);
				data.Write(newData);
				newData.Dispose();
				_socket.Send(data.ToArray());
				data.Dispose();
			} else {
				SendUncompressPacket(data, id);
			}
		}

		public void SendUncompressPacket(MemoryStream data, int id) {
			using (MemoryStream outMs = new MemoryStream()) {
				int size = (int) data.Length + VarIntSize(id);

				outMs.WriteVarInt(size);
				outMs.WriteVarInt(id);
				outMs.Write(data);
				data.Dispose();

				_socket.Send(outMs.ToArray());
			}
		}

		public IEnumerator ReadPacket(MemoryStream packet) {
			if (_threshold > 0) {
				int dataLength = packet.ReadVarInt();

				if (dataLength != 0) {
					if (dataLength >= PARTIAL_DECOMPRESS_MIN_SIZE) {
						MemoryStream outMs = new MemoryStream();
						byte[] buffer = new byte[PARTIAL_DECOMPRESS_BUFFER_SIZE];
						var zOut = new ZlibStream(packet, CompressionMode.Decompress);
						int len;
						while ((len = zOut.Read(buffer, 0, buffer.Length)) > 0) {
							outMs.Write(buffer, 0, len);
                            yield return new PriorityYieldInstruction(PriorityYieldInstruction.Priority.High);
                        }
						zOut.Close();
						outMs.Position = 0;

						packet.Dispose();
						packet = outMs;
					} else {
						byte[] decompressed = ZlibUtils.Decompress(packet, dataLength);
						packet.Dispose();
						packet = new MemoryStream(decompressed);
					}
				}
			}

			ReadUncompressPacket(packet);
		}

		public void ReadUncompressPacket(MemoryStream packet) {
			int packetId = packet.ReadVarInt();

			PacketHandler.Handle(packetId, packet);
			packet.Dispose();
		}

		private static int VarIntSize(int value) {
			int size = 0;
			while ((value & -128) != 0) {
				size++;
				value = (int) (((uint) value) >> 7);
			}

			return ++size;
		}
	}
}