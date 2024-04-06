using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BatteryUI : MonoBehaviour
{
	private readonly string defaultText = "Battery : ";

	[SerializeField]
	private TextMeshProUGUI tmp;

	private void OnValidate()
	{
		if (tmp == null) throw new NullReferenceException();
	}

	private void OnEnable() =>
		PlayerStateMachine.Instance.GetComponent<Battery>().BatteryLevelChanged += OnBatteryChanged;

	private void OnBatteryChanged(float _, float percentage) => tmp.text = defaultText + percentage + "%";

	private void OnDisable()
	{
		if (PlayerStateMachine.Instance != null)
		{
			PlayerStateMachine.Instance.GetComponent<Battery>().BatteryLevelChanged -= OnBatteryChanged;
		}
	}
}