using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CleaningScoreData
{
	private const float fudgeFactor = 0.7f;
	public int MaxScore;
	public int ActualScore;
	public float ScorePercentage => Mathf.Clamp01(((float) ActualScore / ((float) MaxScore * fudgeFactor))) * 100;
	public Texture2D InitialTexture { get; set; }
	public Texture2D RuntimeTexture { get; set; }
}

public class CleaningScore : GenericUnitySingleton<CleaningScore>
{
	public CleaningScoreData ScoreData { get; private set; }
	public static event Action<CleaningScoreData> CleaningScoreGenerated;

	private void Start()
	{
		((DeadState) PlayerStateMachine.Instance.DeadState).Death += OnGameOver;
	}

	public void OnGameOver()
	{
		Texture2D initialTexture = GetComponent<GroundTextureGenerator>().savedTexture;
		Texture2D runtimeTexture = GetComponent<RuntimeGroundTexture>().texture;
		var width = initialTexture.width;
		var height = initialTexture.height;
		var runTimeData = runtimeTexture.GetPixels(0, 0, width, height);

		var initialData = initialTexture.GetPixels(0, 0, width, height);
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

		ScoreData = new CleaningScoreData()
		{
			MaxScore = maxScore,
			ActualScore = actualScore,
			InitialTexture = initialTexture,
			RuntimeTexture = runtimeTexture
		};

		CleaningScoreGenerated?.Invoke(ScoreData);
	}
}