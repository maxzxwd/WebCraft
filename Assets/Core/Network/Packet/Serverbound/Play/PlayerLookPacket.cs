using System.IO;
using UnityEngine;

namespace Network.Packet.Serverbound.Play {
    public class PlayerLookPacket : IPacket {
        public const int PACKET_ID = 0x0F;
        public readonly float Yaw;
        public readonly float Pitch;
        public readonly bool OnGround;

        public PlayerLookPacket(float yaw, float pitch, bool onGround) {
            Yaw = yaw;
            Pitch = pitch;
            OnGround = onGround;
        }

        public PlayerLookPacket(Vector2 rotation, bool onGround)
            : this (rotation.x, rotation.y, onGround) {}

        public void Send(ServerConnection connection) {
            connection.SendPacket(new MemoryStream()
                .WriteFloat(Yaw)
                .WriteFloat(Pitch)
				.WriteBool(OnGround), PACKET_ID);
        }

        public override string ToString() {
            return "PlayerLookPacket[" + Yaw + ", " + Pitch + ", " + OnGround + "]";
        }
    }
}