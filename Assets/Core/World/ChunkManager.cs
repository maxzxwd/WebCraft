using System.IO;
using UnityEngine;

namespace World {
	public class ChunkManager : MonoBehaviour {
		public const int W = 16;
		public const int H = 256;
		public const int D = 16;

		public ChunkSection[] Sections = new ChunkSection[H / ChunkSection.SIZE];
		public readonly byte[] BlockBiomeArray = new byte[256];
        public WorldManager World { get; private set; }
        public Vector3Int Position;
		public Vector3Int WorldPos;
		public void Init(WorldManager world, Vector3Int position) {
			Position = position;
			WorldPos = position * 16;
			World = world;
		}

		public void FillChunk(MemoryStream ms, int primaryBitMask, bool groundUpContinuous) {
			for (int i = 0; i < Sections.Length; i++) {
				ChunkSection section = Sections[i];

				if ((primaryBitMask & 1 << i) == 0) {
					if (groundUpContinuous) {
						Sections[i] = null;
					}
				} else {
					if (section == null) {
						Sections[i] = section = CreateSection(i);
					}

					section.FillSection(ms);
					ms.Read(section.BlocklightArray.Data);

					if (World.Overworld) {
						ms.Read(section.SkylightArray.Data);
					}
					section.Rend();
				}
			}

			if (groundUpContinuous) {
				ms.Read(BlockBiomeArray);
			}
		}

		public static int ChunkPosToInt(int x, int z) {
			return x * 31 + z;
		}

		private ChunkSection CreateSection(int y) {
			GameObject sectionObj = new GameObject();
			sectionObj.transform.parent = this.transform;
			var coll = sectionObj.AddComponent<MeshCollider>();
            //coll.cookingOptions = MeshColliderCookingOptions.;
            MeshRenderer renderer = sectionObj.AddComponent<MeshRenderer>();
            renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
            renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            renderer.sharedMaterials = World.Materials;
			sectionObj.AddComponent<MeshFilter>();
			ChunkSection section = sectionObj.AddComponent<ChunkSection>();
			section.Init(this, y);
			sectionObj.transform.localPosition = new Vector3(0, section.WorldY);
            return section;
		}
	}
}