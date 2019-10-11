using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class UpdateHealthPacket : IPacket {
        public const int PACKET_ID = 0x41;
        public readonly float Health;
        public readonly int Food;
        public readonly float FoodSaturation;

        public UpdateHealthPacket(MemoryStream ms) {
            Health = ms.ReadFloat();
            Food = ms.ReadVarInt();
            FoodSaturation = ms.ReadVarInt();
        }

        public override string ToString() {
            return "UpdateHealthPacket[" + Health + ", " + Food + ", " + FoodSaturation + "]";
        }
    }
}