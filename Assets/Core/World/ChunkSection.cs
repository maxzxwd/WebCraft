using System.IO;
using System.Collections.Generic;
using System;
using World.Renderer;
using UnityEngine;

namespace World {
    public class ChunkSection : MonoBehaviour {
        public const int SIZE = 16;
        public readonly short[,,] Data = new short[SIZE, SIZE, SIZE];
        public readonly NibbleArray BlocklightArray = new NibbleArray();
        public readonly NibbleArray SkylightArray = new NibbleArray();
        public MeshRenderer MeshRenderer {
            get;
            private set;
        }
        public int WorldY {
			get {
				return Y * SIZE;
			}
		}
        public ChunkManager Chunk {
            get {
                return _chunk;
            }
        }
        public int Y {
            get;
            private set;
        }
        private SectionRenderer _sectionRenderer;
        private int _bitsPerBlock;
		private int[] _palette;
        private long[] _longs;
        private ChunkManager _chunk;
        public void Init(ChunkManager chunk, int y) {
            Y = y;
            _chunk = chunk;
            _sectionRenderer = new SectionRenderer(this);
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        public void FillSection(MemoryStream ms) {
            _bitsPerBlock = ms.ReadSByte();

            _palette = new int[ms.ReadVarInt()];
            for (int i = 0; i < _palette.Length; i++) {
                _palette[i] = ms.ReadVarInt();
            }

            _longs = new long[ms.ReadVarInt()];
            for (int i = 0; i < _longs.Length; i++) {
                _longs[i] =  ms.ReadLong();
            }
        }

        public void DecodeLongs() {
            if (_longs == null) {
                return;
            }
            long maxEntryValue = (1L << _bitsPerBlock) - 1;
            for (int j = 0; j < SIZE; j++) {
                int jw = j << 8;
                for (int k = 0; k < SIZE; k++) {
                    int jkw = jw | k << 4;
                    for (int i = 0; i < SIZE; i++) {
                        int index = jkw | i;
                        int bitIndex = index * _bitsPerBlock;
                        int startIndex = bitIndex / 64;
                        int endIndex = ((index + 1) * _bitsPerBlock - 1) / 64;
                        int startBitSubIndex = bitIndex % 64;
                        long shiftedLong = (long)((ulong) _longs[startIndex] >> startBitSubIndex);
                        int rawId;
                        if (startIndex == endIndex) {
                            rawId = (int) (shiftedLong & maxEntryValue);
                        } else {
                            int endBitSubIndex = 64 - startBitSubIndex;
                            rawId = (int) ((shiftedLong | _longs[endIndex] << endBitSubIndex) & maxEntryValue);
                        }

                        if (_bitsPerBlock <= 8) {
                            if (rawId >= 0 && rawId < _palette.Length) {
                                rawId = _palette[rawId];
                            } else {
                                rawId = 0;
                            }
                        }

                        Data[SIZE - 1 - i, j, k] = (short) GameSession.FromMC(rawId);
                    }
                }
            }

            _longs = null;
            _palette = null;
        }

        public void Rend(bool instante = false) {
            if (instante) {
                _sectionRenderer.Rend(gameObject);
            } else {
                Chunk.World.RenderQueue.Add(this);
            }
        }
    }
}   