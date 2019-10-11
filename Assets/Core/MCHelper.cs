using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MCHelper {
	public static int FixAbsoluteX(int x) {
		int cx = x >> 4;
		int bx = x & 0x000F;

		return cx * -16 + (15 - bx);
	}

	public static float FixAbsoluteX(float x) {
		int cx = (int) x >> 4;
		int bx = (int) x & 0x000F;
		float floatPart = -(x - (int) x);

		return cx * -16 + (16 - bx) + floatPart;
	}

	public static double FixAbsoluteX(double x) {
		int cx = (int) x >> 4;
		int bx = (int) x & 0x000F;
		double floatPart = -(x - (int) x);

		return cx * -16 + (16 - bx) + floatPart;
	}
}