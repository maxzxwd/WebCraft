using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World.Colormap {
    public class DefaultColormap : IColorMap {
		private Color32[] _buffer;

        public int GetIndex(float temperature, float rainfall) {
			temperature = Mathf.Clamp(temperature, 0.0F, 1.0F);
        	rainfall = Mathf.Clamp(rainfall, 0.0F, 1.0F);
            rainfall *= temperature;
			int i = (int) ((1.0F - temperature) * 255.0F);
			int j = (int) (rainfall * 255.0F);

			return j * 256 + i;
        }

		public int GetIndex(int biome) {
			switch (biome) {
				case 1:
				case 16:
				case 129:
					return GetIndex(0.8f, 0.4f);
				case 2:
				case 8:
				case 17:
				case 37:
				case 38:
				case 39:
				case 130:
				case 165:
				case 166:
				case 167:
					return GetIndex(2, 0);
				case 3:
				case 25:
				case 34:
				case 131:
				case 162:
					return GetIndex(0.2f, 0.3f);
				case 4:
				case 18:
				case 29:
				case 132:
				case 157:
					return GetIndex(0.7f, 0.8f);
				case 5:
				case 133:
				case 160:
				case 161:
					return GetIndex(0.25f, 0.8f);
				case 6:
				case 134:
					return GetIndex(0.8f, 0.9f);
				case 10:
				case 11:
				case 12:
				case 13:
				case 140:
					return GetIndex(0, 0.5f);
				case 14:
				case 15:
					return GetIndex(0.9f, 1);
				case 19:
					return GetIndex(0.8f, 0.45f);
				case 20:
					return GetIndex(0.8f, 0.3f);
				case 21:
				case 22:
				case 149:
					return GetIndex(0.95f, 0.9f);
				case 23:
				case 151:
					return GetIndex(0.95f, 0.8f);
				case 26:
					return GetIndex(0.05f, 0.3f);
				case 27:
				case 28:
				case 155:
				case 156:
					return GetIndex(0.6f, 0.6f);
				case 30:
				case 31:
				case 158:
					return GetIndex(-0.5f, 0.4f);
				case 32:
				case 33:
					return GetIndex(0.3f, 0.8f);
				case 35:
					return GetIndex(1.2f, 0);
				case 36:
				case 164:
					return GetIndex(1, 0);
				case 163:
					return GetIndex(1.1f, 0);
				default:
					return GetIndex(0.5f, 0.5f);
			}
		}

		public Color32 GetColor(int biome) {
			if (biome == -1) {
				return Color.white;
			}
			return _buffer[GetIndex(biome)];
		}

        public void Load(Texture2D texture) {
			_buffer = new Color32[texture.height * texture.width];
			_buffer = texture.GetPixels32();
        }
    }
}
