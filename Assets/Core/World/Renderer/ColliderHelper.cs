using System;
using System.Collections.Generic;
using UnityEngine;

namespace World.Renderer {
    public class ColliderHelper {
        private static readonly int[] _cubeIndices = {
            0, 2, 3, 0, 3, 1,
            2, 4, 5, 2, 5, 3,
            4, 6, 7, 4, 7, 5,
            6, 0, 1, 6, 1, 7,
            1, 3, 5, 1, 5, 7,
            6, 4, 2, 6, 2, 0
        };

        private static readonly int[] _indices = new int[147456];
        private static readonly Mesh _mesh = new Mesh();

        static ColliderHelper() {
            for (int i = 0; i < _indices.Length; i += 36) {
                for (int k = 0; k < _cubeIndices.Length; k++) {
                    _indices[i + k] = _cubeIndices[k] + i * 8 / 36;
                }
            }
        }

        private readonly List<Vector3> _vertices = new List<Vector3>();

        public void AddCollider(int x, int y, int z, Block block) {
            if (block.ColliderType != ColliderType.None) {
                _vertices.Add(new Vector3(x + block.ColliderMax.x, y + block.ColliderMin.y, z + block.ColliderMax.z));
                _vertices.Add(new Vector3(x + block.ColliderMin.x, y + block.ColliderMin.y, z + block.ColliderMax.z));
                _vertices.Add(new Vector3(x + block.ColliderMax.x, y + block.ColliderMax.y, z + block.ColliderMax.z));
                _vertices.Add(new Vector3(x + block.ColliderMin.x, y + block.ColliderMax.y, z + block.ColliderMax.z));
                _vertices.Add(new Vector3(x + block.ColliderMax.x, y + block.ColliderMax.y, z + block.ColliderMin.z));
                _vertices.Add(new Vector3(x + block.ColliderMin.x, y + block.ColliderMax.y, z + block.ColliderMin.z));
                _vertices.Add(new Vector3(x + block.ColliderMax.x, y + block.ColliderMin.y, z + block.ColliderMin.z));
                _vertices.Add(new Vector3(x + block.ColliderMin.x, y + block.ColliderMin.y, z + block.ColliderMin.z));
            }
        }

        public void UploadMesh(MeshCollider collider) {
            if (_vertices.Count > 0) {

                _mesh.SetVertices(_vertices);

                unsafe {
                    fixed (void* ptr = _indices) {
                        *((UIntPtr*)ptr - 1) = (UIntPtr)(_vertices.Count * 36 / 8);
                        _mesh.SetTriangles(_indices, 0, false);
                        *((UIntPtr*)ptr - 1) = (UIntPtr)(147456);
                    }
                }
                collider.sharedMesh = _mesh;
                _mesh.Clear();
            }
        }

        public void Reset() {
            _vertices.Clear();
            _vertices.TrimExcess();
        }
    }
}