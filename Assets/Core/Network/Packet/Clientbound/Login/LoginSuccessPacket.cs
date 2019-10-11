using System.IO;

namespace Network.Packet.Clientbound.Login {
    public class LoginSuccessPacket : IPacket {
        public const int PACKET_ID = 0x02;
        public readonly string Uuid;
        public readonly string Username;

        public LoginSuccessPacket(MemoryStream ms) {
            Uuid = ms.ReadString();
            Username = ms.ReadString();
        }

        public override string ToString() {
            return "LoginSuccessPacket[" + Uuid + ", " + Username + "]";
        }
    }
}