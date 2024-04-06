using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GroundTextureGeneratorWindow : EditorWindow
{
	private static GroundTextureGenerator generator;
	private Texture2D image;
	private string savePath = "Assets/FloorTexture.png";

	[MenuItem("SoppyMilk/GroundTextureGenerator")]
	public static void GroundTextureGenerator()
	{
		var w = GetWindow<GroundTextureGeneratorWindow>();
		generator = FindObjectOfType<GroundTextureGenerator>();
		w.position = new Rect(w.position.x, w.position.y, 600, 600);
	}

	private void OnGUI()
	{
		if (generator == null)
		{
			GetGenerator();
			if (generator == null)
			{
				EditorGUILayout.LabelField("No Generator in scene");
				return;
			}
		}

		generator.startPosition = EditorGUILayout.Vector3Field(new GUIContent("Position"), generator.startPosition);

		generator.extents = EditorGUILayout.Vector3Field(new GUIContent("Extents"), generator.extents);
		generator.textureSize = EditorGUILayout.Vector2IntField(new GUIContent("TextureSize"), generator.textureSize);

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Show area");
		generator.showArea = EditorGUILayout.Toggle(generator.showArea);
		EditorGUILayout.EndHorizontal();

		generator.traversableTag = EditorGUILayout.TagField("Traversable tag", generator.traversableTag);
		if (GUILayout.Button("Generate"))
		{
			image = generator.Generate();
		}

		if (image != null)
		{
			float size = 512;
			GUILayout.Label(image, GUILayout.Width(size), GUILayout.Height(size));
		}

		savePath = EditorGUILayout.TextField("Save Path:", savePath);

		if (GUILayout.Button("Save Image"))
		{
			if (image != null)
			{
				File.WriteAllBytes(savePath, image.EncodeToPNG());
				AssetDatabase.ImportAsset(savePath);
			}
		}
	}

	private void GetGenerator()
	{
		generator = FindObjectOfType<GroundTextureGenerator>();
	}
}