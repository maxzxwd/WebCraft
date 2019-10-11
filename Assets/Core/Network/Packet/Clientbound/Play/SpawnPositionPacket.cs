using System.IO;
using UnityEngine;

namespace Network.Packet.Clientbound.Play {
    public class SpawnPositionPacket : IPacket {
        public const int PACKET_ID = 0x46;
        public readonly Vector3Int Location;

        public SpawnPositionPacket(MemoryStream ms) {
            Location = ms.ReadPosition();

            Location.x = MCHelper.FixAbsoluteX(Location.x);
        }

        public override string ToString() {
            return "SpawnPositionPacket[" + Location + "]";
        }
    }
}