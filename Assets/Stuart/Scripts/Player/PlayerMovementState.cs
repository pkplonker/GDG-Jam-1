using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
	private PlayerStateMachine statemachine;
	private Transform transform;
	private float currentRotationSpeedMultiplier = 1f;
	private float currentSpeedMultiplier = 1;
	private float currentSpeed => statemachine.MovementStats.MovementSpeed * currentSpeedMultiplier;
	private float currentRotationSpeed => statemachine.MovementStats.RotationSpeed * currentRotationSpeedMultiplier;

	public void EnterState(IStateMachine sm)
	{
		this.statemachine = sm as PlayerStateMachine;
		if (statemachine == null) throw new InvalidCastException();
		transform = statemachine.GetTransform();
	}

	public void ExitState() { }

	public void Tick()
	{
		var input = InputController.Instance.GetPlayerMovement();
		transform.position += transform.rotation * new Vector3(0, 0, input.y * currentSpeed * Time.deltaTime);
		transform.RotateAround(transform.position, transform.up, input.x * currentRotationSpeed * Time.deltaTime);
		Debug.Log(input);
	}
}