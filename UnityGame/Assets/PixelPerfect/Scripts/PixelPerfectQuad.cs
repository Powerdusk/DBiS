using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelPerfectQuad : PixelPerfectBase {

	Texture checkTexture;
	
	new public void LateUpdate() {
		if (checkTexture!=GetComponent<Renderer>().sharedMaterial.mainTexture) {
			offset=new Vector3(
				(((int)GetComponent<Renderer>().sharedMaterial.mainTexture.width)%2==0)?0:PixelPerfect.unitsPerPixel*0.5f,
				(((int)GetComponent<Renderer>().sharedMaterial.mainTexture.height)%2==0)?0:PixelPerfect.unitsPerPixel*0.5f,
				0);
			UpdateScale();
			checkTexture=GetComponent<Renderer>().sharedMaterial.mainTexture;
		}
		base.LateUpdate();
	}

	override public void UpdateScale() {
		if (checkPixelScale!=pixelScale || checkTexture!=GetComponent<Renderer>().sharedMaterial.mainTexture) {
			Transform saveParent=transform.parent;
			transform.parent=null;
			transform.localScale=new Vector3(
				Mathf.Sign(transform.localScale.x)*pixelScale*PixelPerfect.unitsPerPixel*GetComponent<Renderer>().sharedMaterial.mainTexture.width,
				Mathf.Sign(transform.localScale.y)*pixelScale*PixelPerfect.unitsPerPixel*GetComponent<Renderer>().sharedMaterial.mainTexture.height,
				transform.localScale.z);
			transform.parent=saveParent;
			checkPixelScale=pixelScale;
		}
	}

	override protected float GetImageWidth () {return GetComponent<Renderer>().sharedMaterial.mainTexture.width;}
	override protected float GetImageHeight() {return GetComponent<Renderer>().sharedMaterial.mainTexture.height;}
}
