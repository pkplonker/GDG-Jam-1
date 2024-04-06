using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RubbishCollectionUI : MonoBehaviour
{
	private readonly string defaultText = "Rubbish collected: ";

	[SerializeField]
	private TextMeshProUGUI tmp;

	private void OnValidate()
	{
		if (tmp == null) throw new NullReferenceException();
	}

	private void Start() => ScoreChanged(0);

	private void OnEnable() => ScoreController.OnScoreChanged += ScoreChanged;

	private void ScoreChanged(int score) => tmp.text = defaultText + score;

	private void OnDisable() => ScoreController.OnScoreChanged -= ScoreChanged;
}