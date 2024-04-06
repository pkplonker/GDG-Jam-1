using System;
using UnityEngine;

public class DeadState : IState
{
	private PlayerStateMachine statemachine;
	public event Action Death;

	public void EnterState(IStateMachine sm)
	{
		this.statemachine = sm as PlayerStateMachine;
		if (statemachine == null) throw new InvalidCastException();
		Death?.Invoke();
	}

	public void ExitState() { }

	public void Tick()
	{
		Debug.LogWarning("DEAD!");
	}
}