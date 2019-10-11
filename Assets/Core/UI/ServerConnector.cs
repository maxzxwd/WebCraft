using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using World;

namespace UI {
	public class ServerConnector : MonoBehaviour {
		public GameObject LoadingScreen;
		public Slider Slider;
		public InputField LoginField;
		public InputField ServerField;

		public void Connect() {
			StartCoroutine(ConnectAsynchronously());
		}
		
		IEnumerator ConnectAsynchronously() {
			string serverUrl = FixURL(ServerField.text);
			GameSession.Login = LoginField.text;
			GameSession.ServerUrl = serverUrl;
			TextureAtlas.Init();

			LoadingScreen.SetActive(true);
			
			// Download and parse main json
			ResourcesInfo resourcesInfo;
            using (var request = UnityWebRequest.Get("http://" + serverUrl + "/main.json")) {
                request.SendWebRequest();
                while (!request.isDone) {
                    Slider.value = request.downloadProgress;
                    yield return null;
                }
                if (request.isHttpError || request.isNetworkError) {
                    LoadingScreen.SetActive(false);
                    yield break;
                }
                resourcesInfo = JsonUtility.FromJson<ResourcesInfo>('{' + request.downloadHandler.text + '}');
                resourcesInfo.resourcesUrl = FixURL(resourcesInfo.resourcesUrl);
            }

			// Download and parse remapper json
			RemapperInfo remapperInfo;
			using (var request = resourcesInfo.DownloadText(resourcesInfo.remapper)) {
                request.SendWebRequest();
                while (!request.isDone) {
                    Slider.value = request.downloadProgress;
					yield return null;
				}
                if (request.isHttpError || request.isNetworkError) {
                    LoadingScreen.SetActive(false);
					yield break;
				}
				remapperInfo = JsonUtility.FromJson<RemapperInfo>('{' + request.downloadHandler.text + '}');
				GameSession.SetRemapper(remapperInfo.GetFlattenBlocks());
			}

			// Download and apply foliage colormap
			using (var request = resourcesInfo.DownloadTexture(resourcesInfo.foliageColormap)) {
                request.SendWebRequest();
                while (!request.isDone) {
                    Slider.value = request.downloadProgress;
					yield return null;
				}
                if (request.isHttpError || request.isNetworkError) {
                    LoadingScreen.SetActive(false);
					yield break;
				}
				GameSession.FoliageColormap.Load(DownloadHandlerTexture.GetContent(request));
			}

			// Download and apply grass colormap
			using (var request = resourcesInfo.DownloadTexture(resourcesInfo.grassColormap)) {
                request.SendWebRequest();
                while (!request.isDone) {
                    Slider.value = request.downloadProgress;
					yield return null;
				}
                if (request.isHttpError || request.isNetworkError) {
                    LoadingScreen.SetActive(false);
                    yield break;
                }
                GameSession.GrassColormap.Load(DownloadHandlerTexture.GetContent(request));
			}

			// Download and parse blocks json
			BlocksInfo blocksInfo;
			using (var request = resourcesInfo.DownloadText(resourcesInfo.blocks)) {
                request.SendWebRequest();
                while (!request.isDone) {
                    Slider.value = request.downloadProgress;
					yield return null;
				}
                if (request.isHttpError || request.isNetworkError) {
                    LoadingScreen.SetActive(false);
                    yield break;
                }
                blocksInfo = JsonUtility.FromJson<BlocksInfo>('{' + request.downloadHandler.text + '}');
			}

			// Load all blocks
			GameSession.Palette = new Block[blocksInfo.list.Length + 1];
			for (int i = 1; i < blocksInfo.list.Length + 1; i++) {
				TempBlock block = blocksInfo.list[i - 1];
				Vector3[] faces = new Vector3[block.textures.Length];
				for (int j = 0; j < faces.Length; j++) {
					string textureName = block.textures[j];
					using (var request = resourcesInfo.DownloadTexture(textureName)) {
                        request.SendWebRequest();
                        while (!request.isDone) {
                            Slider.value = request.downloadProgress;
							yield return null;
						}
                        if (request.isHttpError || request.isNetworkError) {
                            LoadingScreen.SetActive(false);
                            yield break;
                        }
                        faces[j] = TextureAtlas.AddTexture(textureName, DownloadHandlerTexture.GetContent(request), null);
					}
				}

				RenderType renderType = block.renderType == null
					? RenderType.Block
					: (RenderType) Enum.Parse(typeof(RenderType), block.renderType);

				ShaderType shaderType = block.shaderType == null
					? ShaderType.Block
					: (ShaderType) Enum.Parse(typeof(ShaderType), block.shaderType);

				BiomedType biomed = block.biomed == null
					? BiomedType.None
					: (BiomedType) Enum.Parse(typeof(BiomedType), block.biomed);

				ColliderType colliderType = block.colliderType == null
					? ColliderType.Block
					: (ColliderType) Enum.Parse(typeof(ColliderType), block.colliderType);

				Vector3 colliderMin = new Vector3(block.colliderMinX,
					block.colliderMinY, block.colliderMinZ);

				Vector3 colliderMax = new Vector3(block.colliderMaxX,
					block.colliderMaxY, block.colliderMaxZ);

				Block grassOverlay = null;
				if (block.grassOverlay != null) {
					using (var request = resourcesInfo.DownloadTexture(block.grassOverlay)) {
                        request.SendWebRequest();
                        while (!request.isDone) {
                            Slider.value = request.downloadProgress;
							yield return null;
						}
                        if (request.isHttpError || request.isNetworkError) {
                            LoadingScreen.SetActive(false);
                            yield break;
                        }
                        grassOverlay = new Block("", new Vector3[]{TextureAtlas.AddTexture(
							block.grassOverlay, DownloadHandlerTexture.GetContent(request), null)}, ColliderType.None,
							colliderMin, colliderMax);
					}
				}

				GameSession.Palette[i] = new Block(block.name, faces, colliderType,
					colliderMin, colliderMax, renderType, shaderType, biomed, grassOverlay,
					block.renderParam);
			}

			AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
			while (!operation.isDone) {
				float progress = Mathf.Clamp01(operation.progress / 0.9f);

				Slider.value = progress;

				yield return null;
			}
		}

		private static string FixURL(string url) {
			if (url.EndsWith("/")) {
				return url.Substring(0, url.Length - 1);
			}
			return url;
		}

		[System.Serializable]
		class ResourcesInfo {
			public string name;
			public string resourcesUrl;
			public string remapper;
			public string blocks;
			public string foliageColormap;
			public string grassColormap;

			public UnityWebRequest DownloadText(string path) {
				return UnityWebRequest.Get("http://" + resourcesUrl + "/" + path);
			}

            public UnityWebRequest DownloadTexture(string path)
            {
                return UnityWebRequestTexture.GetTexture("http://" + resourcesUrl + "/" + path);
            }
        }

		[System.Serializable]
		class RemapperInfo {
			public RamapperPair[] blocks;

			public Dictionary<int, int> GetFlattenBlocks() {
				Dictionary<int, int> flatten = new Dictionary<int, int>();
				foreach (RamapperPair pair in blocks) {
					int type = 0;
        			int meta = 0;

					int index = pair.from.IndexOf(":");
					if (index == -1) {
						type = int.Parse(pair.from);
					} else {
						type = int.Parse(pair.from.Substring(0, index));
						meta = int.Parse(pair.from.Substring(index + 1));
					}

					flatten.Add((type << 4) | meta, pair.to);
				}

				return flatten;
			}
		}
		[System.Serializable]
		class RamapperPair {
			public string from;
			public int to;
		}

		[System.Serializable]
		class BlocksInfo {
			public TempBlock[] list;
			public TempAnimation[] animations;
		}
		[System.Serializable]
		class TempBlock {
			public string name;
			public string[] textures;
			public string renderType;
			public string shaderType;
			public string biomed;
			public string grassOverlay;
			public float renderParam = 0;
			public string colliderType;
			public float colliderMinX = 0;
			public float colliderMinY = 0;
			public float colliderMinZ = 0;
			public float colliderMaxX = 1;
			public float colliderMaxY = 1;
			public float colliderMaxZ = 1;
		}
		[System.Serializable]
		class TempAnimation {
			public string texture;
			public int time;
		}
	}
}