using System;
using UnityEngine;

public class TimeController : GenericUnitySingleton<TimeController>
{
	public float elapsedSeconds { get; private set; }
	private bool doUpdate;

	private void Start()
	{
		StartTimer();
		PauseController.Instance.PauseChanged += PauseControllerOnPauseChanged;
	}

	private void OnDisable()
	{
		PauseController.Instance.PauseChanged -= PauseControllerOnPauseChanged;
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