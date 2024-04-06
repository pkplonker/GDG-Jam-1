using System;
using UnityEngine;

public class DeadState : IState
{
	private PlayerStateMachine statemachine;

	public void EnterState(IStateMachine sm)
	{
		this.statemachine = sm as PlayerStateMachine;
		if (statemachine == null) throw new InvalidCastException();
	}

	public void ExitState() { }

	public void Tick()
	{
		Debug.LogWarning("DEAD!");
	}
}