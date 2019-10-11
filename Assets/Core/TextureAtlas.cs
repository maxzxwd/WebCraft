using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public static class TextureAtlas {
    public const int SIZE = 512;
    private static Texture2D _texture;
    private static Dictionary<string, Vector3> _positions;
    private static Dictionary<string, Texture2D> _testures;
    private static Dictionary<string, int> _animationTimes;
    private static int _currentY;
    private static int _currentX;
    private static bool[,] _usedPixels;

    public static void Init() {
        _texture = new Texture2D(SIZE, SIZE, TextureFormat.RGBA32, false);
        _texture.filterMode = FilterMode.Point;
        _positions = new Dictionary<string, Vector3>();
        _testures = new Dictionary<string, Texture2D>();
        _animationTimes = new Dictionary<string, int>();
        _currentY = _currentX = 0; 
        _usedPixels = new bool[SIZE, SIZE];
    }

    public static Vector3 AddTexture(string name, Texture texture, int? animationTime) {
        return AddTexture(name, texture as Texture2D, animationTime);
    }

    public static Vector3 AddTexture(string name, Texture2D texture, int? animationTime) {
        if (_positions.ContainsKey(name)) {
            return _positions[name];
        }

        if (animationTime.HasValue) {
            _animationTimes.Add(name, animationTime.Value);
            _testures.Add(name, texture);
        }
       
        while (_currentY + texture.width <= SIZE) {
            bool haveSpace = true;
            if (_currentX + texture.width > SIZE) {
                haveSpace = false;
            } else {
                for (int i = _currentY; i < _currentY + texture.width; i++) {
                    for (int j = _currentX; j < _currentX + texture.width; j++) {
                        if (_usedPixels[j, i]) {
                            haveSpace = false;
                            break;
                        }

                        _usedPixels[j, i] = true;
                    }
                    if (!haveSpace) {
                        break;
                    }
                }
            }

            if (!haveSpace) {
                _currentX++;

                if (_currentX == SIZE) {
                    _currentX = 0;
                    _currentY++;
                }
            } else {
                break;
            }
        }

        _texture.SetPixels(_currentX, _currentY, texture.width, texture.width, texture.GetPixels());
        float fsize = SIZE;
        Vector3 pos = new Vector4(_currentX / fsize, _currentY / fsize, texture.width / fsize);
        _positions.Add(name, pos);
        return pos;
    }

    public static Texture2D RebuildAtlas(int animationTime) {
        _texture.Apply();
        return _texture;
    }
}