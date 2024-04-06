using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Battery))]
public class BatteryEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		var battery = (Battery) target;
		if (EditorApplication.isPlaying)
		{
			if (GUILayout.Button("Set Max Battery"))
			{
				battery.BatteryLevel = battery.batteryStats.MaxBattery;
			}

			if (GUILayout.Button("Set Low Battery"))
			{
				battery.BatteryLevel = (battery.batteryStats.MaxBattery - battery.batteryStats.MinBattery) / 100;
			}

			if (GUILayout.Button("Set Empty Battery"))
			{
				battery.BatteryLevel = battery.batteryStats.MinBattery;
			}
		}
	}
}