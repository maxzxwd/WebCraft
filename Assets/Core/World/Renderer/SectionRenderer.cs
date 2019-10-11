using System.Collections.Generic;
using UnityEngine;
using System;
using World;

namespace World.Renderer {
    public class SectionRenderer {
		public readonly LightingHelper LightingHelper;
		private readonly ColliderHelper _colliderHelper;
        private readonly List<Vector3> _vertices = new List<Vector3>();
		private readonly List<int>[] _indices = {new List<int>(), new List<int>(),
			new List<int>(), new List<int>()};
		private readonly List<Vector2> _uvs = new List<Vector2>();
        private readonly WorldManager _world;
		public ChunkSection Section {
			get;
			private set;
		}

        public SectionRenderer(ChunkSection section) {
            Section = section;
            _world = section.Chunk.World;
			LightingHelper = new LightingHelper(section.Chunk.World);
			_colliderHelper = new ColliderHelper();
        }

        public void Rend(GameObject go) {
            for (int i = 0; i < ChunkSection.SIZE; i++) {
				for (int j = 0; j < ChunkSection.SIZE; j++) {
                    for (int k = 0; k < ChunkSection.SIZE; k++) {
                        int blockId = Section.Data[i, j, k];

                        if (blockId != 0) {
							Block block = GameSession.Palette[blockId];
							ARenderer renderer = null;
							switch (block.RenderType) {
								case RenderType.Block:
								case RenderType.MippedBlock:
									renderer = new BlockRenderer(this);
									break;
								
								case RenderType.Grass:
									renderer = new GrassRenderer(this);
									break;

								case RenderType.Bed:
									renderer = new BedRenderer(this);
									break;

								case RenderType.Sapling:
									renderer = new SaplingRenderer(this);
									break;

								case RenderType.Fluid:
									renderer = new FluidRenderer(this);
									break;

								case RenderType.Rail:
									renderer = new RailRenderer(this);
									break;							
							}

							renderer.BlockId = blockId;
							renderer.Block = block;
							renderer.SetPosition(i, j, k);
							renderer.Initialize();
							renderer.RenderWestSide();
							renderer.RenderEastSide();
							renderer.RenderUpSide();
							renderer.RenderDownSide();
							renderer.RenderSouthSide();
							renderer.RenderNorthSide();
							if (renderer.Visible) {
                                _colliderHelper.AddCollider(i, j, k, block);
							}
						}
                    }
                }
            }

            Mesh mesh = new Mesh() {
				subMeshCount = _indices.Length,
			};
			mesh.SetVertices(_vertices);
			for (int i = 0; i < _indices.Length; i++) {
				mesh.SetTriangles(_indices[i], i, false);
			}

			MeshCollider collider = go.GetComponent<MeshCollider>();
			_colliderHelper.UploadMesh(collider);
			_colliderHelper.Reset();

			mesh.SetUVs(0, _uvs);
			mesh.SetUVs(1, LightingHelper.Lights);
			mesh.SetColors(LightingHelper.BiomeColors);
            MeshFilter filter = go.GetComponent<MeshFilter>();
            filter.mesh = mesh;
			mesh.UploadMeshData(true);
			
			_vertices.Clear();
			foreach (var indices in _indices) {
				indices.Clear();
			}
            _uvs.Clear();
			LightingHelper.BiomeColors.Clear();
            LightingHelper.Lights.Clear();
			_vertices.TrimExcess();
			foreach (var indices in _indices) {
				indices.TrimExcess();
			}
            _uvs.TrimExcess();
			LightingHelper.BiomeColors.TrimExcess();
            LightingHelper.Lights.TrimExcess();
        }

		public bool IsRenderNeed(Block block, int blockId, int neighborId) {
			if (neighborId == 0) {
				return true;
			}
			Block neighbor = GameSession.Palette[neighborId];
			if (neighbor.RenderType == RenderType.Sapling) {
				return true;
			} else if (neighbor.RenderType == RenderType.Fluid) {
				return block.RenderType != RenderType.Fluid;
			} else if (!neighbor.ClassicBlock && (block.ClassicBlock || blockId != neighborId)) {
				return true;
			} 
			return false;
		}

    	public void GenFace(float cx, float cy, float cz,
							float rx, float ry, float rz,
							float ux, float uy, float uz,
							int rot, Block block, int face,
							float textureRatio = 1,
							float textureSize = 1,
							float textureX = 1,
							float textureY = 1) {
			List<int> indices  = _indices[(int) block.ShaderType];
			int index = _vertices.Count;

			_vertices.AddRange(new Vector3[] {
				new Vector3(cx, cy, cz),
				new Vector3(cx + rx, cy + ry, cz + rz),
				new Vector3(cx + rx + ux, cy + ry + uy, cz + rz + uz),
				new Vector3(cx + ux, cy + uy, cz + uz)
			});
			
			float td = block.Faces[face].z * textureSize;
			float xoff = block.Faces[face].x * textureX;
			float yoff = block.Faces[face].y * textureY;

			switch (Mathf.Abs(rot) % 4) {
				case 0:
					_uvs.AddRange(new Vector2[] {
						new Vector2(td + xoff, yoff),
						new Vector2(xoff, yoff),
						new Vector2(xoff, yoff + td * textureRatio),
						new Vector2(td + xoff, yoff + td * textureRatio)
					});
					break;
				case 1:
					_uvs.AddRange(new Vector2[] {
						new Vector2(xoff, yoff + td * textureRatio),
						new Vector2(td + xoff, yoff + td * textureRatio),
						new Vector2(td + xoff, yoff),
						new Vector2(xoff, yoff)
					});
					break;
				case 2:
					_uvs.AddRange(new Vector2[] {
						new Vector2(xoff, yoff),
						new Vector2(xoff, yoff + td * textureRatio),
						new Vector2(td + xoff, yoff + td * textureRatio),
						new Vector2(td + xoff, yoff)
					});
					break;
				case 3:
					_uvs.AddRange(new Vector2[] {
						new Vector2(td + xoff, yoff + td * textureRatio),
						new Vector2(td + xoff, yoff),
						new Vector2(xoff, yoff),
						new Vector2(xoff, yoff + td * textureRatio)
					});
					break;
			}
			
			if (rot >= 0) {
				indices.AddRange(new int[] {
					index,
					index + 1,
					index + 2,
					index + 2,
					index + 3,
					index
				});
			} else {
				indices.AddRange(new int[] {
					index + 1,
					index,
					index + 2,
					index + 3,
					index + 2,
					index
				});
			}
		}
    }
}