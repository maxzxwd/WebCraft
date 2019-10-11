using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Colormap {
	public interface IColorMap {
		void Load(Texture2D texture);
		int GetIndex(float temperature, float rainfall);
		int GetIndex(int biome);
		Color32 GetColor(int biome);
	}
}
