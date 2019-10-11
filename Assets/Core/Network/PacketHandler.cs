using System.IO;
using Network.Packet;
using Network.Packet.Clientbound.Play;

namespace Network {
    public class PacketHandler {
        public event LoginPacketReceived LoginPacketReceivedEvent;
        public event PlayPacketReceived PlayPacketReceivedEvent; 

        private bool _loginSucces = false;

        public PacketHandler() {
            LoginPacketReceivedEvent += OnLoginSuccessPacketPacket;
        }

        public void Handle(int id, MemoryStream ms) {
            if (_loginSucces) {
                IPacket packet = null;
                switch (id) {
                    case BlockChangePacket.PACKET_ID:
                        packet = new BlockChangePacket(ms);
                        break;
                    case ChatMessagePacket.PACKET_ID:
                        packet = new ChatMessagePacket(ms);
                        break;
                    case UpdateHealthPacket.PACKET_ID:
                        packet = new UpdateHealthPacket(ms);
                        break;
                    case ChunkDataPacket.PACKET_ID:
                        packet = new ChunkDataPacket(ms);
                        break;
                    case DisconnectPacket.PACKET_ID:
                        packet = new DisconnectPacket(ms);
                        break;
                    case KeepAlivePacket.PACKET_ID:
                        packet = new KeepAlivePacket(ms);
                        break;
                    case PlayerAbilitiesPacket.PACKET_ID:
                        packet = new PlayerAbilitiesPacket(ms);
                        break;
                    case PlayerPositionAndLookPacket.PACKET_ID:
                        packet = new PlayerPositionAndLookPacket(ms);
                        break;
                    case SpawnExperienceOrbPacket.PACKET_ID:
                        packet = new SpawnExperienceOrbPacket(ms);
                        break;
                    case SpawnGlobalEntityPacket.PACKET_ID:
                        packet = new SpawnGlobalEntityPacket(ms);
                        break;
                    case SpawnObjectPacket.PACKET_ID:
                        packet = new SpawnObjectPacket(ms);
                        break;
                    case UnloadChunkPacket.PACKET_ID:
                        packet = new UnloadChunkPacket(ms);
                        break;
                    case SpawnPositionPacket.PACKET_ID:
                        packet = new SpawnPositionPacket(ms);
                        break;
                }

                if (packet != null && PlayPacketReceivedEvent != null) {
                    this.PlayPacketReceivedEvent(packet);
                }
            } else {
                IPacket packet = null;
                switch (id) {
                    case Packet.Clientbound.Login.DisconnectPacket.PACKET_ID:
                        packet = new Packet.Clientbound.Login.DisconnectPacket(ms);
                        break;
                    case Packet.Clientbound.Login.LoginSuccessPacket.PACKET_ID:
                        packet = new Packet.Clientbound.Login.LoginSuccessPacket(ms);
                        break;
                    case Packet.Clientbound.Login.SetCompressionPacket.PACKET_ID:
                        packet = new Packet.Clientbound.Login.SetCompressionPacket(ms);
                        break;
                }

                if (packet != null && LoginPacketReceivedEvent != null) {
                    this.LoginPacketReceivedEvent(packet);
                }
            }
            ms.Dispose();
        }

        public void OnLoginSuccessPacketPacket(IPacket packet) {
			var lsp = packet as Packet.Clientbound.Login.LoginSuccessPacket;
			if (lsp != null) {
				_loginSucces = true;
			}
		}

        public delegate void LoginPacketReceived(IPacket packet);
        public delegate void PlayPacketReceived(IPacket packet);
    }
}