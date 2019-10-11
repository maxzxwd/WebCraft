using System.IO;

namespace Network.Packet.Serverbound.Login {
    public class HandshakePacket : IPacket {
        public const int PACKET_ID = 0x00;
        public readonly int ProtocolVersion;
        public readonly string ServerAddress;
        public readonly ushort ServerPort;
        public readonly States NextState;

        public HandshakePacket(int protocolVersion, string serverAddress,
            ushort serverPort, States nextState) {
            ProtocolVersion = protocolVersion;
            ServerAddress = serverAddress;
            ServerPort = serverPort;
            NextState = nextState;
        }

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
                .WriteVarInt(ProtocolVersion)
                .WriteString(ServerAddress)
                .WriteUShort(ServerPort)
                .WriteVarInt((int) NextState), PACKET_ID, false);
        }

        public override string ToString() {
            return "HandshakePacket[" + ProtocolVersion + ", " + ServerAddress + ", " +
                ServerPort + ", " + NextState + "]";
        }

        public enum States {
            Status = 1, Login = 2
        }
    }
}