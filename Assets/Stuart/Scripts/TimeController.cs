using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
	public float elapsedSeconds { get; private set; }
	private bool doUpdate;
	private PauseController pauseController;

	private void Start()
	{
		StartTimer();
		pauseController = FindObjectOfType<PauseController>();
		pauseController.PauseChanged += PauseControllerOnPauseChanged;
	}

	private void OnDisable()
	{
		pauseController.PauseChanged -= PauseControllerOnPauseChanged;
	}

	private void PauseControllerOnPauseChanged(bool obj)
	{
		doUpdate = !obj;
	}

	public void StartTimer()
	{
		elapsedSeconds = 0;
		doUpdate = true;
	}

	public void StopTimer()
	{
		doUpdate = false;
	}

	public void Update()
	{
		if (!doUpdate) return;
		elapsedSeconds += Time.deltaTime;
	}
}