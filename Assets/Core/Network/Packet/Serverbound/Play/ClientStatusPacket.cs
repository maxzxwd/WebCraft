using System.IO;

namespace Network.Packet.Serverbound.Play {
    public class ClientStatusPacket : IPacket {
        public const int PACKET_ID = 0x03;
        private readonly Status _status;

        public ClientStatusPacket(Status status) {
            _status = status;
        }

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
				.WriteVarInt((int) _status), PACKET_ID);
        }

        public override string ToString() {
            return "ClientStatusPacket[" + _status + "]";
        }

        public enum Status {
            PerformRespawn, RequesStats
        }
    }
}