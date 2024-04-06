using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RuntimeGroundTexture))]
public class RuntimeGroundTextureEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var textureGen = (RuntimeGroundTexture) target;
		if (textureGen.texture != null)
		{
			float size = 256;
			GUILayout.Label(textureGen.texture, GUILayout.Width(size), GUILayout.Height(size));
		}
	}
}