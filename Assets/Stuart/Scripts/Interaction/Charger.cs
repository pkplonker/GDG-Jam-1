using System;
using System.Collections;
using UnityEngine;

public class Charger : MonoBehaviour, IHitTarget
{
	private Coroutine cor;

	[SerializeField]
	private float chargeFrequency = 0.2f;

	[SerializeField]
	private float chargeAmount = 0.2f;

	public static event Action ChargeEnded;
	private WaitForSeconds wait;

	private void Start()
	{
		wait = new WaitForSeconds(chargeFrequency);
	}

	private void OnTriggerEnter(Collider other)
	{
		var battery = other.gameObject.GetComponentInParent<Battery>();
		if (battery == null)
		{
			throw new NullReferenceException("WTF you doing");
		}

		StopCor();
		StartCor(battery);
	}

	private void OnTriggerExit(Collider other)
	{
		StopCor();
	}

	private void StartCor(Battery battery)
	{
		cor = StartCoroutine(Charge(battery));
	}

	private void StopCor()
	{
		if (cor != null)
		{
			StopCoroutine(cor);
			cor = null;
		}
	}

	private IEnumerator Charge(Battery battery)
	{
		while (true)
		{
			if (battery.BatteryLevel == battery.batteryStats.MaxBattery)
			{
				break;
			}

			if (!PauseController.Instance.IsPaused) battery.Charge(chargeAmount);

			yield return wait;
		}

		ChargeEnded?.Invoke();
	}
}