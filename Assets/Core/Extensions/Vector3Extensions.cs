using System.Collections;
using UnityEngine;

public static class Vector3Extensions {
	public static Vector3 Fix(this Vector3 vec) {
		return new Vector3(Fix(vec.x), Fix(vec.y), Fix(vec.z));
	}

	public static Vector3 Floor(this Vector3 vec) {
		return new Vector3((int) vec.x, (int) vec.y, (int) vec.z);
	}
	
	public static Vector3Int ToInt(this Vector3 vec) {
		return new Vector3Int((int) vec.x, (int) vec.y, (int) vec.z);
	}

	public static Vector3Int Top(this Vector3Int vec) {
		return vec + new Vector3Int(0, 1, 0);
	}

	public static Vector3Int Down(this Vector3Int vec) {
		return vec - new Vector3Int(0, 1, 0);
	}

	public static Vector3Int Left(this Vector3Int vec) {
		return vec + new Vector3Int(1, 0, 0);
	}

	public static Vector3Int Right(this Vector3Int vec) {
		return vec - new Vector3Int(1, 0, 0);
	}

	public static Vector3Int Back(this Vector3Int vec) {
		return vec + new Vector3Int(0, 0, 1);
	}

	public static Vector3Int Front(this Vector3Int vec) {
		return vec - new Vector3Int(0, 0, 1);
	}

	private static float Fix(float f) {
		if (Mathf.Abs(f % 1) > 0.999f) {
			return (int) f + 1;
		} else if (Mathf.Abs(f % 1) < 0.001) {
			return (int) f;
		}

		return f;
	}
}
