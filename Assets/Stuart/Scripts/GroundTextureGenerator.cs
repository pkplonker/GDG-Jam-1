using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class GroundTextureGenerator : MonoBehaviour
{
	public Texture2D savedTexture;
	public bool showArea { get; set; }
	public Vector3 startPosition { get; set; } = new Vector3(0, 0, 0);

	public Vector3 extents { get; set; } = new Vector3(20, 20, 20);
	public Vector2Int textureSize { get; set; } = new Vector2Int(1024, 1024);
	public string traversableTag { get; set; } = "Traversable";
	public bool bakeOnLoad  = true;

	private void Awake()
	{
		if (bakeOnLoad) Generate();
	}

	public Texture2D Generate()
	{
		var texture = new Texture2D(textureSize.x, textureSize.y, TextureFormat.RGBAFloat, false);

		var startPos = startPosition - new Vector3(extents.x / 2, 0, extents.y / 2);
		var xIncrement = extents.x / textureSize.x;
		var zIncrement = extents.z / textureSize.y;
		for (int x = 0; x < textureSize.x; x++)
		{
			for (int y = 0; y < textureSize.y; y++)
			{
				var pos = startPos + (new Vector3(xIncrement * x, extents.y, zIncrement * y));
				bool traversable = true;
				if (Physics.Raycast(pos, Vector3.down, out var hitInfo))
				{
					if (!hitInfo.collider.CompareTag(traversableTag))
					{
						traversable = false;
					}
				}

				texture.SetPixel(x, y, traversable ? Color.white : Color.black);
			}
		}

		savedTexture = texture;
		texture.Apply();
		return texture;
	}

	private void OnDrawGizmos()
	{
		if (showArea) Gizmos.DrawWireCube(startPosition, extents);
	}
}