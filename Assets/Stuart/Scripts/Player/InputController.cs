using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : GenericUnitySingleton<InputController>
{
	private PlayerInput playerControls;

	public Vector2 GetPlayerMovement()
	{
		return PauseController.Instance.IsPaused
			? new Vector2(0, 0)
			: playerControls.UserInput.Movement.ReadValue<Vector2>();
	}

	protected override void Awake()
	{
		base.Awake();
		playerControls = new PlayerInput();
		playerControls.Enable();
	}
}