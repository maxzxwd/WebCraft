using System.Collections.Generic;
using System.IO;

namespace Network.Packet.Clientbound.Play {
    public class ChunkDataPacket : IPacket {
        public const int PACKET_ID = 0x20;
        public readonly int ChunkX;
        public readonly int ChunkZ;
        public readonly bool GroundUpContinuous;
        public readonly int PrimaryBitMask;
        public byte[] Data;
        //public List<NbtCompound> BlockEntities;

        public ChunkDataPacket(MemoryStream ms) {
            ChunkX = -ms.ReadInt();
            ChunkZ = ms.ReadInt();
            GroundUpContinuous = ms.ReadBool();
            PrimaryBitMask = ms.ReadVarInt();
            Data = new byte[ms.ReadVarInt()];
            ms.Read(Data, 0, Data.Length);
            
            int nbtCount = ms.ReadVarInt();
            /*
            BlockEntities = new List<NbtCompound>(nbtCount);
            for (int i = 0; i < nbtCount; i++) {
                try {
                    NbtFile nbtFile = new NbtFile();
                    nbtFile.LoadFromStream(ms, NbtCompression.AutoDetect, null);
                    BlockEntities.Add(nbtFile.RootTag);
                } catch (EndOfStreamException e) {
                }
            }
            */
        }

        public override string ToString() {
            return "ChunkDataPacket[" + ChunkX + ", " + ChunkZ + ", " +
                GroundUpContinuous + ", " + PrimaryBitMask + "]";
        }
    }
}