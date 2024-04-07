using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState2ElectricBoogaloo : IState, IMovement
{
	public event Action<float> MoveAmount;
	public event Action<float> RotateAmount;
	private PlayerStateMachine statemachine;
	private Transform transform;

	PeterTestControls input = null;
	Vector2 movement = Vector2.zero;

	Rigidbody rb;

	public void EnterState(IStateMachine sm)
	{
		this.statemachine = sm as PlayerStateMachine;
		if (statemachine == null) throw new InvalidCastException();
		transform = statemachine.GetTransform();

		rb = statemachine.GetComponent<Rigidbody>();

		input = statemachine.input;

		input.Enable();
		input.PlayerTest.Movement.performed += OnMoveKey;
		input.PlayerTest.Movement.canceled += OnMoveCancel;

		input.PlayerTest.Cameras.performed += OnCameraInput;
	}

	public void ExitState()
	{
		input.Disable();
		input.PlayerTest.Movement.performed -= OnMoveKey;
		input.PlayerTest.Movement.canceled -= OnMoveCancel;

		input.PlayerTest.Cameras.performed -= OnCameraInput;
	}

	public void Tick() { }

	public void FixedTick()
	{
		if (!PauseController.Instance.IsPaused)
		{
			//Debug.Log(transform.up);

			transform.RotateAround(transform.position, transform.up,
				movement.y < 0.1f && movement.y > -0.1f
					? movement.x * statemachine.MovementStats.rotateSpeed
					: movement.y * movement.x * statemachine.MovementStats.rotateSpeed);
			if (movement.x != 0f) RotateAmount.Invoke(movement.x);

			if (!statemachine.MovementStats.allowBackwards)
			{
				movement.y = Mathf.Clamp(movement.y, 0, 1);
			}

			Vector3 rotatedMove = transform.rotation * new Vector3(0, 0, movement.y);
			rb.AddForce(rotatedMove.normalized * statemachine.MovementStats.accelerationSpeed, ForceMode.Force);
			if (movement.y != 0f) MoveAmount.Invoke(movement.y);

			if (rb.velocity.magnitude >
			    statemachine.MovementStats.maxSpeed) //if horizontal velocity is greater than set speed
			{
				Vector3 limitedVel =
					rb.velocity.normalized * statemachine.MovementStats.maxSpeed; //sets velocity ot be max speed
				rb.velocity = new Vector3(limitedVel.x, limitedVel.y, limitedVel.z);
			}
		}

		rb.AddForce(-transform.up * statemachine.MovementStats.gravity, ForceMode.Force);
	}

	private void OnMoveKey(InputAction.CallbackContext context)
	{
		movement = context.ReadValue<Vector2>();
	}

	private void OnMoveCancel(InputAction.CallbackContext context)
	{
		movement = Vector2.zero;
	}

	private void OnCameraInput(InputAction.CallbackContext context)
	{
		switch (context.control.name)
		{
			case "1":
				statemachine.firstPersonCamera.SetActive(true);
				statemachine.topDownCamera.SetActive(false);
				statemachine.thirdPersonCamera.SetActive(false);
				break;
			case "2":
				statemachine.firstPersonCamera.SetActive(false);
				statemachine.topDownCamera.SetActive(true);
				statemachine.thirdPersonCamera.SetActive(false);
				break;
			case "3":
				statemachine.firstPersonCamera.SetActive(false);
				statemachine.topDownCamera.SetActive(false);
				statemachine.thirdPersonCamera.SetActive(true);
				break;
		}
	}
}