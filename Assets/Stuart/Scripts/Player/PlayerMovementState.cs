using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovementState : IState, IMovement
{
	public event Action<float> MoveAmount;
	public event Action<float> RotateAmount;
	private PlayerStateMachine statemachine;
	private Transform transform;
	private float currentRotationSpeedMultiplier = 1f;
	private float currentSpeedMultiplier = 1;
	//private float currentSpeed => statemachine.MovementStats.MovementSpeed * currentSpeedMultiplier;
	//private float currentRotationSpeed => statemachine.MovementStats.RotationSpeed * currentRotationSpeedMultiplier;

	public void EnterState(IStateMachine sm)
	{
		this.statemachine = sm as PlayerStateMachine;
		if (statemachine == null) throw new InvalidCastException();
		transform = statemachine.GetTransform();
	}

	public void ExitState() { }

	public void Tick()
	{
		//var input = InputController.Instance.GetPlayerMovement();
		//if (input.y >0)
		//{
		//	var amount = input.y * currentSpeed * Time.deltaTime;
		//	transform.position += transform.rotation * new Vector3(0, 0, amount);
		//	MoveAmount?.Invoke(amount);
		//}

		//if (input.x != 0)
		//{
		//	var amount = input.x * currentRotationSpeed * Time.deltaTime;
		//	transform.RotateAround(transform.position, transform.up, amount);
		//	RotateAmount?.Invoke(amount);
		//}
	}

    public void FixedTick() { }
}