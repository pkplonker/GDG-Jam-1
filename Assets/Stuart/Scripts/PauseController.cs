using System;

public class PauseController : GenericUnitySingleton<PauseController>
{
	public event Action<bool> PauseChanged;

	private bool isPaused;

	public bool IsPaused
	{
		get => isPaused;
		set
		{
			if (isPaused != value)
			{
				isPaused = value;
				PauseChanged?.Invoke(isPaused);
			}
		}
	}
}