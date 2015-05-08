using UnityEngine;
using UnityEditor;

public class SpritePostProcessor : AssetPostprocessor {
	void OnPreprocessTexture() {
		TextureImporter importer = assetImporter as TextureImporter;
		
		if (importer.textureType==TextureImporterType.Sprite) {
			importer.textureFormat = TextureImporterFormat.RGBA32;
			importer.mipmapEnabled = false;
			importer.spritePixelsPerUnit=PixelPerfect.pixelsPerUnit;
			importer.filterMode=FilterMode.Point;
			importer.spritePivot=new Vector2(0, 1);
			importer.anisoLevel=0;
			importer.borderMipmap = false;
			importer.mipmapFilter=TextureImporterMipFilter.BoxFilter;
			importer.wrapMode=TextureWrapMode.Clamp;
		}
	}

}