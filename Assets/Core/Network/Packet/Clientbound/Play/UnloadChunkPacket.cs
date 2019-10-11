using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class UnloadChunkPacket : IPacket {
        public const int PACKET_ID = 0x1D;
        public readonly int ChunkX;
        public readonly int ChunkZ;

        public UnloadChunkPacket(MemoryStream ms) {
            ChunkX = -ms.ReadInt();
            ChunkZ = ms.ReadInt();
        }

        public override string ToString() {
            return "UnloadChunkPacket[" + ChunkX + ", " + ChunkZ + "]";
        }
    }
}