using System.IO;
using UnityEngine;

namespace Network.Packet.Clientbound.Play {
    public class BlockChangePacket : IPacket {
        public const int PACKET_ID = 0x0B;
        public readonly Vector3Int Location;
        public readonly int BlockId;

        public BlockChangePacket(MemoryStream ms) {
            Location = ms.ReadPosition();
            Location.x = MCHelper.FixAbsoluteX(Location.x);
            int t = ms.ReadVarInt();
            BlockId = GameSession.FromMC(t);
        }

        public override string ToString() {
            return "BlockChangePacket[" + Location + ", " + BlockId + "]";
        }
    }
}