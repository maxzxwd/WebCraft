using UnityEngine;

namespace World {
	public class Block {
		public readonly string Name;
		public readonly Vector3[] Faces;
		public readonly Block GrassOverlay;
		public readonly float RenderParam;
		public readonly BiomedType Biomed;
		public readonly RenderType RenderType;
		public readonly ShaderType ShaderType;
		public readonly bool ClassicBlock;
		public readonly ColliderType ColliderType;
		public readonly Vector3 ColliderMin;
		public readonly Vector3 ColliderMax;

		public Block(string name, Vector3[] faces, ColliderType colliderType,
			Vector3 colliderMin, Vector3 colliderMax, RenderType renderType = RenderType.Block,
			ShaderType shaderType = ShaderType.Block, BiomedType biomed = BiomedType.None,
			Block grassOverlay = null, float renderParam = 0) {
			Name = name;
			ColliderType = colliderType;
			ColliderMin = colliderMin;
			ColliderMax = colliderMax;
			RenderType = renderType;
			ShaderType = shaderType;
			Biomed = biomed;
			GrassOverlay = grassOverlay;
			RenderParam = renderParam;

			if (faces.Length == 1) {
				Faces = new Vector3[]{faces[0], faces[0], faces[0],
					faces[0], faces[0], faces[0]};
			} else if (faces.Length == 3) {
				Faces = new Vector3[]{faces[0], faces[0], faces[1],
					faces[1], faces[2], faces[2]};
			} else {
				Faces = faces;
			}

			ClassicBlock = shaderType == ShaderType.Block &&
				(renderType == RenderType.Block || renderType == RenderType.Grass);
		}
	}
	public enum RenderType {
		Block, MippedBlock, Grass, Sapling, Fluid, Bed, Rail
	}
	public enum ShaderType {
		Block, Leaves, Transparent
	}
	public enum BiomedType {
		None, Grass, Foliage, Birch, Spruce
	}
	public enum ColliderType {
		None, Block, Water, Lava, Web
	}
}