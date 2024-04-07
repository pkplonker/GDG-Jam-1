using System;

public class ScoreManager : GenericUnitySingleton<ScoreManager>
{
	public event Action<ScoreData> FinalScore;
	public PlayerOverlay overlay;

	private void Start()
	{
		((DeadState) PlayerStateMachine.Instance.DeadState).Death += OnGameOver;
		//subscribe to any other "game over" events

		overlay.FinishedGame += OnGameOver;
	}

	private void OnGameOver() => CalculateFinalScore();

	private void CalculateFinalScore()
	{
		FinalScoreData = new ScoreData
		{
			RubbishGathered = RubbishCollectionController.Instance.RubbishGathered,
			ElapsedTime = TimeController.Instance.elapsedSeconds
		};
		CleaningScore.Instance.OnGameOver();
		FinalScoreData.CleaningScoreData = CleaningScore.Instance.ScoreData;
		//Add score for knocking shit over
		FinalScore?.Invoke(FinalScoreData);
	}

	private void OnDisable() { }

	public ScoreData FinalScoreData;
}

public struct ScoreData
{
	public int RubbishGathered { get; set; }
	public CleaningScoreData CleaningScoreData { get; set; }
	public float ElapsedTime { get; set; }

	public int FinalScore => CalculateFinalScore();

	private int CalculateFinalScore()
	{
		//whatever this is math should be.
		return 0;
	}
}