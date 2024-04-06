using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CleaningScoreData
{
	public int maxScore;
	public int actualScore;
	public float ScorePercentage => ((float) actualScore / (float) maxScore) * 100;
}

public class CleaningScore : MonoBehaviour
{
	public static event Action<CleaningScoreData> CleaningScoreGenerated;

	private void Start()
	{
		((DeadState) PlayerStateMachine.Instance.DeadState).Death += OnDeath;
	}

	private void OnDeath()
	{
		Texture2D initialTexture = GetComponent<GroundTextureGenerator>().savedTexture;
		Texture2D runtimeTexture = GetComponent<RuntimeGroundTexture>().texture;
		var width = initialTexture.width;
		var height = initialTexture.height;
		var scoreData = new CleaningScoreData();
		var initialData = initialTexture.GetPixels(0, 0, width, height);
		var runTimeData = runtimeTexture.GetPixels(0, 0, width, height);
		int maxScore = 0;
		int actualScore = 0;
		for (int x = 0; x < initialTexture.width; x++)
		{
			for (int y = 0; y < initialTexture.height; y++)
			{
				int oneDindex = (x * width) + y;
				if (initialData[oneDindex] == Color.white) maxScore++;
				if (runTimeData[oneDindex] == Color.black) actualScore++;
			}
		}

		scoreData.maxScore = maxScore;
		scoreData.actualScore = actualScore;
		CleaningScoreGenerated?.Invoke(scoreData);
	}
}