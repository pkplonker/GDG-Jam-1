using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndOfRoundDisplayBehaviour : UIComponent
{
	public TextMeshProUGUI timeElapsed;
	public TextMeshProUGUI rubbishCollected;
	public TextMeshProUGUI itemsBroken;
	public TextMeshProUGUI travesersalPercentage;

	public RawImage traversalMap;
	public Image finalGrade;
	public TextMeshProUGUI finalGradeText;

	protected override void SetVals()
	{
		ScoreData scores = ScoreManager.Instance.FinalScoreData;

		if (scores != null)
		{
			int minutes = TimeSpan.FromSeconds(scores.ElapsedTime).Minutes;
			int seconds = TimeSpan.FromSeconds(scores.ElapsedTime).Seconds;

			timeElapsed.text = minutes.ToString("00") + ":" + seconds.ToString("00");
			rubbishCollected.text = scores.RubbishGathered + "/" + scores.MaxRubbish.ToString();
			itemsBroken.text = scores.DamagedItems.ToString();
			if (scores.CleaningScoreData != null)
				travesersalPercentage.text = (scores.CleaningScoreData.ScorePercentage).ToString("F0") + "%";

			finalGradeText.text = scores.FinalScore.ToString();
			var texture = scores.CleaningScoreData.RuntimeTexture;

			traversalMap.texture = texture;
		}

		//Set Final Grade
	}

	public void OnContinueGame()
	{
		Debug.Log("Continuing Game");
	}

	public void OnReturnToMenu()
	{
		Debug.Log("Returning to Menu");
		SceneManager.LoadScene("MainMenu");
	}
}