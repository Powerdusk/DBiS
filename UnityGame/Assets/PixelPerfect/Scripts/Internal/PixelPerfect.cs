using UnityEngine;
using System.Collections;

public static class PixelPerfect {
	public static int pixelScale {get {if (pixelScale_!=0) {return pixelScale_;} else {return MonoBehaviour.FindObjectOfType<PixelPerfectCamera>().pixelScale;}} set {pixelScale_=value;}}
	public static int pixelScale_=0;
	public static int pixelsPerUnit=16;
	public static float unitsPerPixel=0.0625f;
	[System.NonSerialized]
	public static bool requestPostRender=false;
	
	public static void SetPixelPerfect(int importPixelsPerUnit, int pixelZoom) {
		pixelsPerUnit=importPixelsPerUnit;
		unitsPerPixel=(1f/(float)pixelsPerUnit);
		pixelScale=pixelZoom;
	}
}
