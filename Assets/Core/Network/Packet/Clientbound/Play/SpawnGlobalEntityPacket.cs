using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class SpawnGlobalEntityPacket : IPacket {
        public const int PACKET_ID = 0x02;
        public readonly int EntityId;
        public readonly int Type;
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public SpawnGlobalEntityPacket(MemoryStream ms) {
            EntityId = ms.ReadInt();
            Type = ms.ReadSByte();
            X = ms.ReadDouble();
            Y = ms.ReadDouble();
            Z = ms.ReadDouble();
        }

        public override string ToString() {
            return "SpawnGlobalEntityPacket[" + EntityId + ", " + Type + ", " +
                X + ", " + Y + ", " + Z + "]";
        }
    }
}