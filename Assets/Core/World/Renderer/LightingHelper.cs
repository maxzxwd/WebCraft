using System.Collections.Generic;
using UnityEngine;
using World;

namespace World.Renderer {
    /*
    * A of color used for brightness
    */
    public class LightingHelper {
        private static readonly Vector3Int[][] _cornerNeighbors = {
            new Vector3Int[] {
				new Vector3Int(0, -1, -1),
                new Vector3Int(0, 1, -1),
                new Vector3Int(0, 1, 1),
                new Vector3Int(0, -1, 1)
            },
			new Vector3Int[] {
                new Vector3Int(0, -1, -1),
                new Vector3Int(0, -1, 1),
                new Vector3Int(0, 1, 1),
                new Vector3Int(0, 1, -1)
            },
            new Vector3Int[] {
                new Vector3Int(-1, 0, -1),
                new Vector3Int(-1, 0, 1),
                new Vector3Int(1, 0, 1),
                new Vector3Int(1, 0, -1)
            },
			 new Vector3Int[] {
				new Vector3Int(-1, 0, -1),
                new Vector3Int(1, 0, -1),
                new Vector3Int(1, 0, 1),
                new Vector3Int(-1, 0, 1),
            },
			new Vector3Int[] {
				new Vector3Int(-1, -1, 0),
                new Vector3Int(1, -1, 0),
                new Vector3Int(1, 1, 0),
                new Vector3Int(-1, 1, 0)
            },
            new Vector3Int[] {
                new Vector3Int(-1, -1, 0),
                new Vector3Int(-1, 1, 0),
                new Vector3Int(1, 1, 0),
                new Vector3Int(1, -1, 0)
            }
		};
        private WorldManager _world;
        public List<Color32> BiomeColors = new List<Color32>();
		public List<Vector2> Lights = new List<Vector2>();

        public LightingHelper(WorldManager world) {
            _world = world;
        }

		public void SetLightWest(int x, int y, int z, BiomedType biomed, bool ao = true) {
			SetLight(x, y, z, 0, biomed, 0.56f, 0.0305f, ao);
        }

        public void SetLightEast(int x, int y, int z, BiomedType biomed, bool ao = true) {
			SetLight(x, y, z, 1, biomed, 0.56f, 0.0305f, ao);
        }

		public void SetLightUp(int x, int y, int z, BiomedType biomed, bool ao = true) {
			SetLight(x, y, z, 2, biomed, 0.9f, 0.049f, ao);
		}

        public void SetLightDown(int x, int y, int z, BiomedType biomed, bool ao = true) {
			SetLight(x, y, z, 3, biomed, 0.50f, 0.0275f, ao);
		}

		public void SetLightSouth(int x, int y, int z, BiomedType biomed, bool ao = true) {
			SetLight(x, y, z, 4, biomed, 0.71f, 0.038f, ao);
		}

		public void SetLightNorth(int x, int y, int z, BiomedType biomed, bool ao = true) {
			SetLight(x, y, z, 5, biomed, 0.71f, 0.038f, ao);
		}

		private void SetLight(int x, int y, int z, int neighborsIndex,
			BiomedType biomed, float brightness, float mod, bool ao) {
			Color32 biomeColor;
			if (biomed == BiomedType.None) {
				biomeColor = new Color32(255, 255, 255, 255);
			} else { 
				int biome = _world.GetBiome(x, y, z);

				switch (biomed) {
					case BiomedType.Foliage:
						biomeColor = GameSession.FoliageColormap.GetColor(biome);
						break;

					case BiomedType.Birch:
						biomeColor = GameSession.BirchColormap.GetColor(biome);
						break;
					
					case BiomedType.Spruce:
						biomeColor = GameSession.SpruceColormap.GetColor(biome);
						break;
						
					default:
						biomeColor = GameSession.GrassColormap.GetColor(biome);
						break;
				}
			}
			biomeColor.a = (byte) (brightness * 255);
			BiomeColors.Add(biomeColor);
			BiomeColors.Add(biomeColor);
			BiomeColors.Add(biomeColor);
			BiomeColors.Add(biomeColor);

			Vector2 light;
            float blockLight;
            float skyLight;
            _world.GetLight(x, y, z, out blockLight, out skyLight);
			if (ao) {
				foreach (Vector3Int vec in _cornerNeighbors[neighborsIndex]) {
					float blockLight1;
					float skyLight1;
					_world.GetLight(x + vec.x, y + vec.y, z + vec.z, out blockLight1, out skyLight1);

					float blockLight2;
					float skyLight2;
					if (vec.x == 0) {
						_world.GetLight(x, y + vec.y, z, out blockLight2, out skyLight2);
					} else {
						_world.GetLight(x + vec.x, y, z, out blockLight2, out skyLight2);
					}

					float blockLight3;
					float skyLight3;
					if (vec.z == 0) {
						_world.GetLight(x, y + vec.y, z, out blockLight3, out skyLight3);
					} else {
						_world.GetLight(x, y, z + vec.z, out blockLight3, out skyLight3);
					}
					
					blockLight1 = (blockLight1 + blockLight2 + blockLight3 + blockLight) / 4.0f;
					skyLight1 = (skyLight1 + skyLight2 + skyLight3 + skyLight) / 4.0f;

					blockLight1 = (15 - blockLight1) * mod;
					skyLight1 = (15 - skyLight1) * mod;
					light = new Vector2(skyLight1, blockLight1);
					
					Lights.Add(light);
				}
			} else {
				blockLight = (15 - blockLight) * mod;
				skyLight = (15 - skyLight) * mod;
				light = new Vector2(skyLight, blockLight);
				Lights.Add(light);
				Lights.Add(light);
				Lights.Add(light);
				Lights.Add(light);
			}
		}
    }
}