using System.IO;

namespace Network.Packet.Serverbound.Play {
    public class PlayerPacket : IPacket {
        public const int PACKET_ID = 0x0C;
        public readonly bool OnGround;

        public PlayerPacket(bool onGround) {
            OnGround = onGround;
        }

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
				.WriteBool(OnGround), PACKET_ID);
        }

        public override string ToString() {
            return "PlayerPacket[" + OnGround + "]";
        }
    }
}