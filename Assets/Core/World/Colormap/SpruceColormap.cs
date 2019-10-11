using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Colormap {
    public class SpruceColormap : IColorMap {
		private Color[] _buffer;

        public int GetIndex(float temperature, float rainfall) {
			return 0;
        }

		public int GetIndex(int biome) {
			return 0;
		}

		public Color32 GetColor(int biome) {
			return new Color32(97, 153, 97, 255);
		}

        public void Load(Texture2D texture) {
        }
    }
}
