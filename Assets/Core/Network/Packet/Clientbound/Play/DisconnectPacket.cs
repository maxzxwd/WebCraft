using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class DisconnectPacket : IPacket {
        public const int PACKET_ID = 0x1A;
        public readonly string Reason;

        public DisconnectPacket(MemoryStream ms) {
            Reason = ms.ReadString();
        }

        public override string ToString() {
            return "DisconnectPacket[" + Reason + "]";
        }
    }
}