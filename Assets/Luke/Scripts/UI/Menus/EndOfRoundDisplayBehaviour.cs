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
	public GameObject continueButton;

	public List<Image> stars;
	private ScoreManager scoreManager;

	protected override void Start()
	{
		scoreManager = FindObjectOfType<ScoreManager>();

		base.Start();
	}

	protected override void SetVals()
	{
		ScoreData scores = scoreManager.FinalScoreData;

		if (scores != null)
		{
			int minutes = TimeSpan.FromSeconds(scores.ElapsedTime).Minutes;
			int seconds = TimeSpan.FromSeconds(scores.ElapsedTime).Seconds;

			timeElapsed.text = minutes.ToString("00") + ":" + seconds.ToString("00");
			rubbishCollected.text = scores.RubbishGathered + "/" + scores.MaxRubbish.ToString();
			itemsBroken.text = scores.DamagedItems.ToString();
			if (scores.CleaningScoreData != null)
				travesersalPercentage.text = (scores.CleaningScoreData.ScorePercentage).ToString("F0") + "%";

			//finalGradeText.text = scores.FinalScore.ToString();
			var texture = scores.CleaningScoreData.RuntimeTexture;

			traversalMap.texture = texture;
		}

		//Set Final Grade

		int finalStars = 1;
		float finalScore = scores.FinalScore;
		if (finalScore < 2000) finalStars = 1;
		else if (finalScore >= 2000 && finalScore < 4000) finalStars = 2;
		else if (finalScore >= 4000 && finalScore < 6000) finalStars = 3;
		else if (finalScore >= 6000 && finalScore < 8000) finalStars = 4;
		else if (finalScore >= 8000) finalStars = 5;


		for (int i = 0; i<stars.Count;i++)
        {
			stars[i].gameObject.SetActive(false);
        }


		for (int i = 0; i < finalStars; i++)
		{
			stars[i].gameObject.SetActive(true);
		}

		if (SceneManager.GetActiveScene().name == "Level2")
		{
			continueButton.SetActive(false);
		}
	}

	public void OnContinueGame()
	{
		SceneManager.LoadScene("Level2");
	}

	public void OnReturnToMenu()
	{
		Debug.Log("Returning to Menu");
		SceneManager.LoadScene("MainMenu");
	}
}