using System.IO;

namespace Network.Packet.Serverbound.Play {
    public class KeepAlivePacket : IPacket {
        public const int PACKET_ID = 0x0B;
        public readonly long KeepAliveID;

        public KeepAlivePacket(long keepAliveID) {
            KeepAliveID = keepAliveID;
        }

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
				.WriteLong(KeepAliveID), PACKET_ID);
        }

        public override string ToString() {
            return "KeepAlivePacket[" + KeepAliveID + "]";
        }
    }
}