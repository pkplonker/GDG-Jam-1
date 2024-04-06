using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : GenericUnitySingleton<InputController>
{
	private PlayerInput playerControls;
	public Vector2 GetPlayerMovement() => playerControls.UserInput.Movement.ReadValue<Vector2>();

	protected override void Awake()
	{
		base.Awake();
		playerControls = new PlayerInput();
		playerControls.Enable();
	}

	private void OnEnable() { }
}