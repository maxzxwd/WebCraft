using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class KeepAlivePacket : IPacket {
        public const int PACKET_ID = 0x1F;
        public readonly long KeepAliveID;

        public KeepAlivePacket(MemoryStream ms) {
            KeepAliveID = ms.ReadLong();
        }

        public override string ToString() {
            return "KeepAlivePacket[" + KeepAliveID + "]";
        }
    }
}