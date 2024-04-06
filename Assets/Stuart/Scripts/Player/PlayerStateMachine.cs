using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
	protected IState movementState;

	[SerializeField]
	public MovementStats MovementStats;

	private void Start()
	{
		movementState = new PlayerMovementState();

		ChangeState(movementState);
	}

	protected override void Update()
	{
		currentState.Tick();
	}

	public override void ChangeState(IState state)
	{
		currentState?.ExitState();
		currentState = state;
		currentState.EnterState(this);
	}
}