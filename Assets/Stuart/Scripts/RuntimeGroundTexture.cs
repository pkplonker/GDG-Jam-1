using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeGroundTexture : MonoBehaviour
{
	[HideInInspector]
	public Texture2D texture;
	public Vector2Int textureSize { get; set; } = new Vector2Int(1024, 1024);

	[SerializeField]
	private GameObject target;

	[SerializeField]
	private float targetRadius = 0.75f;

	private GroundTextureGenerator generator;
	private Vector3 startPos;
	private float xIncrement;
	private float zIncrement;

	private void Awake()
	{
		generator = GetComponent<GroundTextureGenerator>();
	}

	private void Start()
	{
		texture = new Texture2D(textureSize.x, textureSize.y, TextureFormat.RGBAFloat, false);
		startPos = generator.startPosition - new Vector3(generator.extents.x / 2, 0, generator.extents.y / 2);
		xIncrement = generator.extents.x / textureSize.x;
		zIncrement = generator.extents.y / textureSize.y;
	}

	private void Update()
	{
		UpdateTexture();
	}

	private void UpdateTexture()
	{
		var targetPos = target.transform.position;
		var distance = targetPos - startPos;
		var xDelta = distance.x / xIncrement;
		var yDelta = distance.z / zIncrement;
		var centreIndex = new Vector2Int(Mathf.FloorToInt(xDelta), Mathf.FloorToInt(yDelta));
		if (centreIndex.x > textureSize.x || centreIndex.x < 0 || centreIndex.y > textureSize.y ||
		    centreIndex.y < 0) return;
		var xStart = Mathf.FloorToInt((targetRadius / xIncrement) / 2);
		var yStart = Mathf.FloorToInt((targetRadius / xIncrement) / 2);

		for (var x = xStart * -1; x < xStart; x++)
		{
			for (var y = yStart * -1; y < yStart; y++)
			{
				var pos = new Vector2Int(centreIndex.x - xStart + x, centreIndex.y - yStart + y);
				if (Vector2.Distance(pos, centreIndex) <= targetRadius / xIncrement / 2)
				{
					texture.SetPixel(pos.x, pos.y, Color.black);
				}
			}
		}

		texture.Apply();
	}
}