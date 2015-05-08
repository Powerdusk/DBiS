using UnityEngine;
using System.Collections;

public class PixelPerfectSprite : PixelPerfectBase {

	SpriteRenderer spriteRenderer {get{return (spriteRenderer_!=null)?spriteRenderer_:spriteRenderer_=GetComponent<SpriteRenderer>();}}
	SpriteRenderer spriteRenderer_;

	Sprite checkSprite;

	new public void LateUpdate() {
		if (checkSprite!=spriteRenderer.sprite) {
			if ((spriteRenderer.bounds.center-transform.position).magnitude==0) {
				offset=new Vector3(
					(((int)spriteRenderer.sprite.rect.width)%2==0)?0:PixelPerfect.unitsPerPixel*0.5f,
					(((int)spriteRenderer.sprite.rect.height)%2==0)?0:PixelPerfect.unitsPerPixel*0.5f,
					0);
			} else {
				offset=Vector3.zero;
			}
			checkSprite=spriteRenderer.sprite;
		}
		base.LateUpdate();
	}

	override protected float GetImageWidth () {return spriteRenderer.sprite.rect.width;}
	override protected float GetImageHeight() {return spriteRenderer.sprite.rect.height;}

}
