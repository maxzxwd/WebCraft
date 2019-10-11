using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class PlayerAbilitiesPacket : IPacket {
        public const int PACKET_ID = 0x2C;
        public readonly int Flags;
        public readonly float FlyingSpeed;
        public readonly float FieldOfViewModifier;

        public PlayerAbilitiesPacket(MemoryStream ms) {
            Flags = ms.ReadSByte();
            FlyingSpeed = ms.ReadFloat();
            FieldOfViewModifier = ms.ReadFloat();
        }

        public override string ToString() {
            return "PlayerAbilitiesPacket[" + Flags + ", " + FlyingSpeed + ", " +
                FieldOfViewModifier + "]";
        }
    }
}