using System;
using UnityEngine;

public class Battery : MonoBehaviour
{
	private IMovement movement;

	private float batteryLevel;
	public BatteryStats batteryStats;

	public event Action<float, float> BatteryLevelChanged;
	public event Action BatteryEmpty;
	public event Action BatteryFull;
	public float BatteryPercentage => (BatteryLevel / batteryStats.MaxBattery) * 100;

	public float BatteryLevel
	{
		get => batteryLevel;
		set
		{
			if (batteryLevel != value)
			{
				batteryLevel = Mathf.Clamp(value, batteryStats.MinBattery, batteryStats.MaxBattery);
				BatteryLevelChanged?.Invoke(batteryLevel, BatteryPercentage);
				if(BatteryLevel == batteryStats.MaxBattery) BatteryFull?.Invoke();
				if(BatteryLevel== batteryStats.MinBattery) BatteryEmpty?.Invoke();
			}
		}
	}

	void Start()
	{
		BatteryLevel = batteryStats.StartBatteryLevel;
	}

	private void OnEnable()
	{
		movement = PlayerStateMachine.Instance.MovementState as IMovement;
		if (movement == null) throw new NullReferenceException();
		movement.MoveAmount += OnMove;
		movement.RotateAmount += OnRotate;
	}

	private void OnDisable()
	{
		movement.MoveAmount -= OnMove;
		movement.RotateAmount -= OnRotate;
	}

	private void OnMove(float amount)
	{
		BatteryLevel -= (Mathf.Abs(amount) * batteryStats.MovementCost);
	}

	private void OnRotate(float amount)
	{
		BatteryLevel -= (Mathf.Abs(amount) * batteryStats.RotationCost);
	}

	public void Charge(float amount)
	{
		BatteryLevel += amount;
	}
}