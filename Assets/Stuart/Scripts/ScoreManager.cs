using System;
using UnityEngine;

public class ScoreManager : GenericUnitySingleton<ScoreManager>
{
	public event Action<ScoreData> FinalScore;
	public PlayerOverlay overlay;
	public EndOfRoundDisplayBehaviour endOfRoundDisplayBehaviour;
	public ScoreData FinalScoreData;
	private int objectsBroken;

	private void Start()
	{
		((DeadState) PlayerStateMachine.Instance.DeadState).Death += OnGameOver;
		//subscribe to any other "game over" events
		overlay.FinishedGame += OnGameOver;
		Breakable.ObjectBroken += OnObjectBroken;
	}

	private void OnObjectBroken()
	{
		objectsBroken++;
	}

	private void OnGameOver() => CalculateFinalScore();

	private void CalculateFinalScore()
	{
		FinalScoreData = new ScoreData
		{
			RubbishGathered = RubbishCollectionController.Instance.RubbishGathered,
			MaxRubbish = RubbishCollectionController.Instance.MaxRubbish,
			ElapsedTime = TimeController.Instance.elapsedSeconds
		};
		CleaningScore.Instance.OnGameOver();
		FinalScoreData.CleaningScoreData = CleaningScore.Instance.ScoreData;
		FinalScoreData.DamagedItems = objectsBroken;
		FinalScore?.Invoke(FinalScoreData);
		endOfRoundDisplayBehaviour.DisplayComponent(endOfRoundDisplayBehaviour, true);
	}

	private void OnDisable()
	{
		((DeadState) PlayerStateMachine.Instance.DeadState).Death -= OnGameOver;
		overlay.FinishedGame -= OnGameOver;
		Breakable.ObjectBroken -= OnObjectBroken;
	}
}

public struct ScoreData
{
	public int MaxRubbish;
	public int RubbishGathered { get; set; }
	public float RubbishGatheredPercentage => ((float) RubbishGathered / (float) MaxRubbish) * 100;
	public CleaningScoreData CleaningScoreData { get; set; }
	public float ElapsedTime { get; set; }

	public int FinalScore => Mathf.FloorToInt(
		((RubbishGatheredPercentage * CleaningScoreData.ScorePercentage) / DamagedItems) *
		((60 * 5) - ElapsedTime));

	public int DamagedItems { get; set; }
}