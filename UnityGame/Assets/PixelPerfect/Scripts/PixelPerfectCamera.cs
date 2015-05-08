using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour {

	public bool smoothScroll=true;
	public int pixelsPerUnit=16;
	[Range(0,1)]public float customOffsetX=0;
	[Range(0,1)]public float customOffsetY=0;
	float checkCustomOffsetX, checkCustomOffsetY;
	[Range(1,16)]public int pixelScale=1;

	[HideInInspector]public int checkedPixelZoom;
	float checkedOrthoSize;
	float currentWidth, currentHeight;
	float targetHeight {get{return GetComponent<Camera>().pixelHeight;}}
	float targetWidth  {get{return GetComponent<Camera>().pixelWidth ;}}

	void Update () {
		AdjustSize();
	}
	
	Vector3 savePosition, checkPosition, fixedPosition;
	
	void OnPreRender() {
		if ((checkPosition-transform.position).magnitude>0 || checkCustomOffsetX!=customOffsetX || checkCustomOffsetY!=customOffsetY) {
			checkPosition=transform.position;
			savePosition=transform.position;

			float gridSize=smoothScroll?PixelPerfect.unitsPerPixel/pixelScale:PixelPerfect.unitsPerPixel;

			fixedPosition=new Vector3(
				Mathf.Round((transform.position.x)/gridSize)*gridSize,
				Mathf.Round((transform.position.y)/gridSize)*gridSize,
				transform.position.z);

//			if (pixelScale%2==0) {
//				if (targetWidth%2==0)  {fixedPosition+=Vector3.right*PixelPerfect.unitsPerPixel*0.5f/pixelScale;}
//				if (targetHeight%2==0) {fixedPosition+=Vector3.up*PixelPerfect.unitsPerPixel*0.5f/pixelScale;}
//			} else {
//				if (targetWidth%2==0)  {fixedPosition+=Vector3.right*PixelPerfect.unitsPerPixel*0.5f;}
//				if (targetHeight%2==0) {fixedPosition+=Vector3.up*PixelPerfect.unitsPerPixel*0.5f;}
//			}
			

			fixedPosition+=new Vector3(customOffsetX, customOffsetY)*PixelPerfect.unitsPerPixel;
			checkCustomOffsetX=customOffsetX;
			checkCustomOffsetY=customOffsetY;

			transform.position=fixedPosition;
			Debug.DrawLine(transform.position, savePosition);
		} else {
			if (transform.position!=fixedPosition) {
				transform.position=fixedPosition;
			}
		}
	}
	
	void OnPostRender() {
		transform.position=savePosition;
	}
	
	public void AdjustSize() {

		if (targetWidth!=currentWidth || targetHeight!=currentHeight || checkedPixelZoom!=pixelScale) {
			PixelPerfect.SetPixelPerfect(pixelsPerUnit, pixelScale);
			GetComponent<Camera>().orthographicSize = (float)((targetHeight / (PixelPerfect.pixelsPerUnit * PixelPerfect.pixelScale)) * 0.5d);		
			currentWidth=targetWidth;
			currentHeight=targetHeight;
			checkedPixelZoom=pixelScale;
		}
	}

}