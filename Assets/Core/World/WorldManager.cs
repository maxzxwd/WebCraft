using System;
using System.Collections.Generic;
using UnityEngine;

namespace World {
	public class WorldManager : MonoBehaviour {
		[Range(0f, 1f)]
		public readonly Dictionary<int, ChunkManager> Chunks =
			new Dictionary<int, ChunkManager>();
		public readonly bool Overworld = true;
		public Material[] Materials;
		public readonly HashSet<ChunkSection> RenderQueue = new HashSet<ChunkSection>();

		public void Start() {
			foreach (Material material in Materials) {
				material.mainTexture = TextureAtlas.RebuildAtlas(0);
			}
		}
		
		public void TryRendFromQueue(int centerX, int centerY, int centerZ) {
			ChunkSection bestSection = null;
			ChunkSection[] bestSectionSides = new ChunkSection[6];
			float bestDistance = 999999;
			foreach (ChunkSection section in RenderQueue) {
				ChunkManager chunkLeft = GetChunk(section.Chunk.Position.Left(), false);
				if (chunkLeft == null) {
					continue;
				}
				ChunkManager chunkRight = GetChunk(section.Chunk.Position.Right(), false);
				if (chunkRight == null) {
					continue;
				}
				ChunkManager chunkFront = GetChunk(section.Chunk.Position.Front(), false);
				if (chunkFront == null) {
					continue;
				}
				ChunkManager chunkBack = GetChunk(section.Chunk.Position.Back(), false);
				if (chunkBack == null) {
					continue;
				}

				float distance = Vector3.Distance(new Vector3(centerX, 0, centerZ),
					section.Chunk.Position);

				int yDist = Math.Abs(centerY - section.Y);
				if (yDist > 2) {
					yDist *= 4;
				}

				distance += yDist;

				if (distance < bestDistance) {
					bestDistance = distance;
					bestSection = section;
					bestSectionSides[0] = chunkLeft.Sections[section.Y];
					bestSectionSides[1] = chunkRight.Sections[section.Y];
					bestSectionSides[2] = chunkFront.Sections[section.Y];
					bestSectionSides[3] = chunkBack.Sections[section.Y];
					if (section.Y > 0) {
						bestSectionSides[4] = section.Chunk.Sections[section.Y - 1];
					} else {
						bestSectionSides[4] = null;
					}
					if (section.Y < 15) {
						bestSectionSides[5] = section.Chunk.Sections[section.Y + 1];
					} else {
						bestSectionSides[5] = null;
					}
				}
			}
			if (bestSection != null) {
				foreach (ChunkSection sectionSide in bestSectionSides) {
					sectionSide?.DecodeLongs();
				}
				bestSection.DecodeLongs();
				bestSection.Rend(true);
				RenderQueue.Remove(bestSection);
			}
			
		}
		
		public ChunkManager GetChunk(Vector3Int vec, bool autoLoad = true) {
			return GetChunk(vec.x, vec.z, autoLoad);
		}

		public ChunkManager GetChunk(int chunkX, int chunkZ, bool autoLoad = true) {
			int chunkInt = ChunkManager.ChunkPosToInt(chunkX, chunkZ);
			ChunkManager chunk = null;
			if (Chunks.ContainsKey(chunkInt)) {
				chunk = Chunks[chunkInt];
			} else if (autoLoad) {
				GameObject chunkObj = new GameObject();
				chunkObj.transform.parent = this.transform;
				chunk = chunkObj.AddComponent<ChunkManager>();
				chunk.Init(this, new Vector3Int(chunkX, 0, chunkZ));
				Chunks.Add(chunkInt, chunk);
				chunkObj.transform.position = chunk.WorldPos;
				chunkObj.isStatic = true;
			}
			
			return chunk;
		}

		public void UnloadChunk(int chunkX, int chunkZ) {
			int chunkInt = ChunkManager.ChunkPosToInt(chunkX, chunkZ);

			if (Chunks.ContainsKey(chunkInt)) {
				ChunkManager chunk = Chunks[chunkInt];
				foreach (ChunkSection section in chunk.Sections) {
					if (section != null) {
						RenderQueue.Remove(section);
					}
				}

				Chunks.Remove(chunkInt);
				Destroy(chunk.gameObject);
				Resources.UnloadUnusedAssets();
			}
		}

		public ChunkSection GetSection(int chunkX, int sectionY, int chunkZ) {
			if (sectionY < 0 || sectionY > 15) {
				return null;
			}
			ChunkManager chunk = GetChunk(chunkX, chunkZ, false);
			if (chunk == null) {
				return null;
			}

			return chunk.Sections[sectionY];
		}

		public int GetBlock(int x, int y, int z) {
			if (y < 0 || y >= ChunkManager.H) {
				return 0;
			}
			int cx = x >> 4;
			int cz = z >> 4;
			
			ChunkManager chunk = GetChunk(cx, cz, false);
			if ((object) chunk == null) {
				return 0;
			}

			int cy = y >> 4;
			ChunkSection section = chunk.Sections[cy];

			if ((object) section == null) {
				return 0;
			}

			int bx = x & 0x000F;
			int by = y & 0x000F;
			int bz = z & 0x000F;
			
			return section.Data[bx, by, bz];
		}
		
		public int GetBiome(int x, int y, int z) {
			if (y < 0 || y >= ChunkManager.H) {
				return 127;
			}
			int cx = x >> 4;
			int cz = z >> 4;

			ChunkManager chunk = GetChunk(cx, cz, false);
			if ((object) chunk == null) {
				return 127;
			}

			int cy = y >> 4;
			ChunkSection section = chunk.Sections[cy];
			if ((object) section == null) {
				return 127;
			}

			int bx = x & 0x000F;
			int bz = z & 0x000F;

			return section.Chunk.BlockBiomeArray[bz * 16 | bx];
		}

		public void GetLight(int x, int y, int z, out float block, out float sky) {
			if (y < 0 || y >= ChunkManager.H) {
				block = sky = 15;
				return;
			}
			int cx = x >> 4;
			int cz = z >> 4;
			
			ChunkManager chunk = GetChunk(cx, cz, false);
			if ((object) chunk == null) {
				block = sky = 15;
				return;
			}

			int cy = y >> 4;
			ChunkSection section = chunk.Sections[cy];
			if ((object) section == null) {
				block = sky = 15;
				return;
			}

			int bx = x & 0x000F;
			int by = y & 0x000F;
			int bz = z & 0x000F;

			block = section.BlocklightArray[bx, by, bz];
			sky = section.SkylightArray[bx, by, bz];

			// For better AO
			int blockId = section.Data[bx, by, bz];
			if (blockId != 0 && GameSession.Palette[blockId].ClassicBlock) {
				block -= 2;
				sky -= 2;	
			}	
		}
	}
}