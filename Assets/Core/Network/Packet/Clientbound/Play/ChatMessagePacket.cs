using System.IO;
using System.Text.RegularExpressions;

namespace Network.Packet.Clientbound.Play {
    public class ChatMessagePacket : IPacket {
        private static Regex _simpleParser = new Regex("\"text\"\\s*:\\s*\"(.*?)(\"\\s*,|\"})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public const int PACKET_ID = 0x0F;
        public readonly string JsonData;
        public readonly int Position;

        public ChatMessagePacket(MemoryStream ms) {
            JsonData = ms.ReadString();
            Position = ms.ReadSByte();
        }

        public string GetSimpleText() {
            MatchCollection matches = _simpleParser.Matches(JsonData);

            string result = "";
            foreach (Match match in matches) {
                if (match.Success) {
                    result += match.Groups[1].Value;
                }
            }

            return result;
        }

        public override string ToString() {
            return "ChatMessagePacket[" + JsonData + ", " + Position + "]";
        }
    }
}