using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerStateMachine : StateMachine
{
	public IState MovementState { get; protected set; }
	public IState DeadState { get; protected set; }

	[SerializeField]
	public MovementStats MovementStats;

    public static PlayerStateMachine Instance;
	private Battery battery;

	public PeterTestControls input;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(this);
		}

		MovementState = new PlayerMovementState2ElectricBoogaloo();
		DeadState = new DeadState();
        input = new PeterTestControls();

        ChangeState(MovementState);
		battery = GetComponent<Battery>();
		battery.BatteryEmpty += OnBatteryEmpty;
	}

	private void OnBatteryEmpty() => ChangeState(DeadState);

	protected override void Update() => currentState.Tick();

	public override void ChangeState(IState state)
	{
		currentState?.ExitState();
		currentState = state;
		currentState.EnterState(this);
	}
}