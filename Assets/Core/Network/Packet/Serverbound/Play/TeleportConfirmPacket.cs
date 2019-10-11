using System.IO;

namespace Network.Packet.Serverbound.Play {
    public class TeleportConfirmPacket : IPacket {
        public const int PACKET_ID = 0x00;
        public readonly int TeleportId;

        public TeleportConfirmPacket(int teleportId) {
            TeleportId = teleportId;
        }

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
				.WriteVarInt(TeleportId), PACKET_ID);
        }

        public override string ToString() {
            return "TeleportConfirmPacket[" + TeleportId + "]";
        }
    }
}