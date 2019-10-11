using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class SpawnObjectPacket : IPacket {
        public const int PACKET_ID = 0x00;
        public readonly int EntityId;
        public readonly Uuid UniqueId;
        public readonly int Type;
        public readonly double X;
        public readonly double Y;
        public readonly double Z;
        public readonly int Pitch;
        public readonly int Yaw;
        public readonly int VelocityX;
        public readonly int VelocityY;
        public readonly int VelocityZ;

        public SpawnObjectPacket(MemoryStream ms) {
            EntityId = ms.ReadInt();
            UniqueId = ms.ReadUuid();
            Type = ms.ReadSByte();
            X = ms.ReadDouble();
            Y = ms.ReadDouble();
            Z = ms.ReadDouble();
            Pitch = ms.ReadShort();
            Yaw = ms.ReadShort();
            VelocityX = ms.ReadShort();
        }

        public override string ToString() {
            return "SpawnObjectPacket[" + EntityId + ", " + UniqueId + ", " + Type + ", " +
                X + ", " + Y + ", " + Z + ", " + Pitch + ", " + Yaw + ", " + VelocityX +
                ", " + VelocityY + ", " + VelocityZ + "]";
        }
    }
}