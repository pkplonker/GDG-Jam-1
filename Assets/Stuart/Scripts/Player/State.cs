using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : IState
{
	protected IStateMachine StateMachine;
	public virtual void EnterState(IStateMachine sm) => StateMachine = sm;

	public void ExitState()
	{
		VirtualStateExit();
		StateMachine = null;
	}

	protected abstract void VirtualStateExit();
	public abstract void Tick();

	public abstract void FixedTick();
}