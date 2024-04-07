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
	private Vector3 deltaCalculatedPos;

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
		var normalisedPosition = targetPos - startPos;
		var xDelta = normalisedPosition.x / xIncrement;
		var yDelta = normalisedPosition.z / zIncrement;
		var centreIndex = new Vector2Int(Mathf.CeilToInt(xDelta), Mathf.CeilToInt(yDelta));
		var xStart = Mathf.CeilToInt((targetRadius / xIncrement) / 2);
		var yStart = Mathf.CeilToInt((targetRadius / zIncrement) / 2);

		deltaCalculatedPos = new Vector3(startPos.x, 0, startPos.z) +
		                     new Vector3(centreIndex.x * xIncrement, 0, centreIndex.y * zIncrement);
		float maxDistance = targetRadius / xIncrement; 

		for (var x = xStart * -1; x < xStart; x++)
		{
			for (var y = yStart * -1; y < yStart; y++)
			{
				var pos = new Vector2Int(centreIndex.x + x, centreIndex.y + y);
				if (pos.x > textureSize.x || pos.x < 0 || pos.y > textureSize.y ||
				    pos.y < 0) continue;
				var distance = Vector2.Distance(pos, centreIndex);
				if (distance <= (targetRadius / xIncrement) / 2)
				{
					texture.SetPixel(pos.x, pos.y, Color.black);
				}
			}
		}

		texture.Apply();
	}
}