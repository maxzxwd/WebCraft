using System.IO;

namespace Network.Packet.Clientbound.Login {
    public class SetCompressionPacket : IPacket {
        public const int PACKET_ID = 0x03;
        public readonly int Threshold;

        public SetCompressionPacket(MemoryStream ms) {
            Threshold = ms.ReadVarInt();
        }

        public override string ToString() {
            return "SetCompressionPacket[" + Threshold + "]";
        }
    }
}