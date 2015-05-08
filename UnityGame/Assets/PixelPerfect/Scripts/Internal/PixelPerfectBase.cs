using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfectBase : MonoBehaviour {
	[Range(1,10)]public int  pixelScale=1;
	public bool scaledSnap=false;
	[Tooltip("Destroys this script during runtime (performance improvement)")]
	public bool isStatic=false;
	public AnchorType anchorType;
	public Camera  anchorCamera;
	public Vector2 anchorOffset;
	
	protected Vector3 fixedPosition;
	protected Vector3 checkPosition;
	protected Vector2 checkAnchorOffset;
	protected float   checkPixelScale;
	protected float   checkOrthoSize;
	protected AnchorType checkAnchorType;
	protected Vector3 offset=Vector3.zero;
	
	GameObject spriteParent;
	
	float snapFactor {get{ return scaledSnap ? pixelScale : 1 ;}}

	void Start() {
		if (isStatic && Application.isPlaying) {
			Destroy(this);
		}
	}

	public void LateUpdate() {
		UpdateScale();
		UpdatePosition();
	}
	
	public virtual void UpdateScale() {
		if (checkPixelScale!=pixelScale) {
			Transform saveParent=transform.parent;
			transform.parent=null;
			transform.localScale=new Vector3(
				Mathf.Sign(transform.localScale.x)*pixelScale,
				Mathf.Sign(transform.localScale.y)*pixelScale,
				transform.localScale.z);
			transform.parent=saveParent;
			checkPixelScale=pixelScale;
		}
	}
	
	public virtual void UpdatePosition() {
		if (transform.position!=checkPosition) {
			fixedPosition=new Vector3(
				Mathf.Round((transform.position.x+offset.x)/(PixelPerfect.unitsPerPixel*snapFactor))*PixelPerfect.unitsPerPixel*snapFactor,
				Mathf.Round((transform.position.y+offset.y)/(PixelPerfect.unitsPerPixel*snapFactor))*PixelPerfect.unitsPerPixel*snapFactor,
				transform.position.z)-offset;
			
			transform.position=fixedPosition;
			checkPosition=transform.position;
		}
		
	}

	protected virtual float GetImageWidth () {return 1;}
	protected virtual float GetImageHeight() {return 1;}

}

public enum AnchorType {Parent, CameraUpperLeft, CameraUpperMiddle, CameraUpperRight, CameraMiddleLeft, CameraMiddleCenter, CameraMiddleRight, CameraLowerLeft, CameraLowerCenter, CameraLowerRight}
