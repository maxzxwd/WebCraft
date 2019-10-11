using System.IO;
using UnityEngine;

namespace Network.Packet.Serverbound.Play {
    public class PlayerPositionPacket : IPacket {
        public const int PACKET_ID = 0x0D;

        public readonly double X;
        public readonly double FeetY;
        public readonly double Z;
        public readonly bool OnGround;

        public PlayerPositionPacket(double x, double feetY, double z, bool onGround) {
            X = MCHelper.FixAbsoluteX(x);
            FeetY = feetY;
            Z = z;
            OnGround = onGround;
        }

        public PlayerPositionPacket(Vector3 position, bool onGround)
            : this (position.x, position.y, position.z, onGround) {}

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
                .WriteDouble(X)
                .WriteDouble(FeetY)
                .WriteDouble(Z)
				.WriteBool(OnGround), PACKET_ID);
        }

        public override string ToString() {
            return "PlayerPositionPacket[" + X + ", " + FeetY + ", " + Z + ", " + OnGround + "]";
        }
    }
}