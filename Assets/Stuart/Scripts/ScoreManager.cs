using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public event Action<ScoreData> FinalScore;
	public PlayerOverlay overlay;
	public EndOfRoundDisplayBehaviour endOfRoundDisplayBehaviour;
	public ScoreData FinalScoreData;
	private int objectsBroken;
	private PauseController pauseController;
	private RubbishCollectionController rubbishCollectionController;
	private TimeController timeController;
	private CleaningScore cleaningScore;

	[SerializeField]
	private float deathEndDelay = 2f;

	private void Start()
	{
		((DeadState) PlayerStateMachine.Instance.DeadState).Death += () => OnGameOver(true);
		//subscribe to any other "game over" events
		overlay.FinishedGame += () => OnGameOver(false);
		Breakable.ObjectBroken += OnObjectBroken;
		pauseController = FindObjectOfType<PauseController>();
		rubbishCollectionController = FindObjectOfType<RubbishCollectionController>();
		timeController = FindObjectOfType<TimeController>();
		cleaningScore = FindObjectOfType<CleaningScore>();
	}

	private void OnObjectBroken()
	{
		objectsBroken++;
	}

	private void OnGameOver(bool death) => CalculateFinalScore(death);

	private void CalculateFinalScore(bool death)
	{
		pauseController.IsPaused = true;
		FinalScoreData = new ScoreData
		{
			RubbishGathered = rubbishCollectionController.RubbishGathered,
			MaxRubbish = rubbishCollectionController.MaxRubbish,
			ElapsedTime = timeController.elapsedSeconds
		};
		cleaningScore.OnGameOver();
		FinalScoreData.CleaningScoreData = cleaningScore.ScoreData;
		FinalScoreData.DamagedItems = objectsBroken;
		FinalScoreData.CompleteData = true;
		FinalScore?.Invoke(FinalScoreData);
		if (death) StartCoroutine(Cor());
		else endOfRoundDisplayBehaviour.DisplayComponent(endOfRoundDisplayBehaviour, true);
	}

	private IEnumerator Cor()
	{
		yield return new WaitForSeconds(deathEndDelay);
		endOfRoundDisplayBehaviour.DisplayComponent(endOfRoundDisplayBehaviour, true);

	}

	private void OnDisable()
	{
		Breakable.ObjectBroken -= OnObjectBroken;
	}
}

public class ScoreData
{
	public bool CompleteData = false;
	public int MaxRubbish;
	public int RubbishGathered { get; set; }
	public float RubbishGatheredPercentage => ((float) RubbishGathered / (float) MaxRubbish) * 100;
	public CleaningScoreData CleaningScoreData { get; set; }
	public float ElapsedTime { get; set; }

	public int FinalScore => !CompleteData
		? 0
		: Mathf.FloorToInt(
			((RubbishGatheredPercentage * CleaningScoreData.ScorePercentage) /
			 Mathf.Clamp(DamagedItems, 1, float.MaxValue)));

	public int DamagedItems { get; set; }
}