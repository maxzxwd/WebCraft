using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class PlayerPositionAndLookPacket : IPacket {
        public const int PACKET_ID = 0x2F;
        public readonly double X;
        public readonly double Y;
        public readonly double Z;

        public readonly float Yaw;
        public readonly float Pitch;

        public readonly EnumFlags Flags;
        public readonly int TeleportId;

        public PlayerPositionAndLookPacket(MemoryStream ms) {
            X = ms.ReadDouble();
            Y = ms.ReadDouble();
            Z = ms.ReadDouble();

            Yaw = ms.ReadFloat();
            Pitch = ms.ReadFloat();

            Flags = (EnumFlags) ((int) ms.ReadByte());

            TeleportId = ms.ReadVarInt();

            // Fix MC x
            if (Flags.HasFlag(EnumFlags.X)) {
                X = -X;
            } else {
                X = MCHelper.FixAbsoluteX(X);
            }
        }

        public override string ToString() {
            return "PlayerPositionAndLookPacket[" + X + ", " + Y + ", " + Z + ", " +
                Yaw + ", " + Pitch + ", " + Flags + ", " + TeleportId + "]";
        }

        public enum EnumFlags {
            X = 0x01,
            Y = 0x02,
            Z = 0x04,
            XRot = 0x08,
            ZRot = 0x10
        }
    }
}