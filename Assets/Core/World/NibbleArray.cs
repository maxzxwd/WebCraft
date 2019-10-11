using System;

namespace World {
    public class NibbleArray {
        public readonly byte[] Data;

        public NibbleArray() {
            Data = new byte[2048];
        }

        public NibbleArray(byte[] storageArray) {
            Data = storageArray;

            if (storageArray.Length != 2048) {
                throw new ArgumentException("NibbleArray should be 2048 bytes not: " + storageArray.Length);
            }
        }

        public int this[int x, int y, int z] {
            get {
                return this[GetCoordinateIndex(ChunkSection.SIZE - x - 1, y, z)];
            }
        }

        public int this[int index] {
            get {
                int i = GetNibbleIndex(index);
                return (IsLowerNibble(index) ? Data[i] : Data[i] >> 4) & 15;
            }
        }
        
        private static int GetCoordinateIndex(int x, int y, int z) {
            return y << 8 | z << 4 | x;
        }

        private static int GetNibbleIndex(int index) {
            return index >> 1;
        }

        private static bool IsLowerNibble(int index) {
            return (index & 1) == 0;
        }
    }
}