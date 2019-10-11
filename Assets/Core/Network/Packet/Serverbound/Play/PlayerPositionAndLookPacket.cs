using System.IO;
using UnityEngine;

namespace Network.Packet.Serverbound.Play {
    public class PlayerPositionAndLookPacket : IPacket {
        public const int PACKET_ID = 0x0E;

        public readonly double X;
        public readonly double FeetY;
        public readonly double Z;
        public readonly float Yaw;
        public readonly float Pitch;
        public readonly bool OnGround;

        public PlayerPositionAndLookPacket(double x, double feetY, double z, float yaw,
            float pitch, bool onGround) {
            X = MCHelper.FixAbsoluteX(x);
            FeetY = feetY;
            Z = z;
            Yaw = yaw;
            Pitch = pitch;
            OnGround = onGround;
        }

        public PlayerPositionAndLookPacket(Vector3 position, Vector2 rotation, bool onGround)
            : this (position.x, position.y, position.z, rotation.x, rotation.y, onGround) {}

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
                .WriteDouble(X)
                .WriteDouble(FeetY)
                .WriteDouble(Z)
                .WriteFloat(Yaw)
                .WriteFloat(Pitch)
				.WriteBool(OnGround), PACKET_ID);
        }

        public override string ToString() {
            return "PlayerPositionAndLookPacket[" + X + ", " + FeetY + ", " + Z + ", " +
                Yaw + ", " + Pitch + ", " + OnGround + "]";
        }
    }
}