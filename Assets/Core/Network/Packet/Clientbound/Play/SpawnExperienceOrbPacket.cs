using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class SpawnExperienceOrbPacket : IPacket {
        public const int PACKET_ID = 0x01;
        public readonly int EntityId;
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly int Count;

        public SpawnExperienceOrbPacket(MemoryStream ms) {
            EntityId = ms.ReadInt();
            X = ms.ReadDouble();
            Y = ms.ReadDouble();
            Z = ms.ReadDouble();
            Count = ms.ReadShort();
        }

        public override string ToString() {
            return "SpawnExperienceOrbPacket[" + EntityId + ", " + X + ", " +
                Y + ", " + Z + ", " + Count + "]";
        }
    }
}