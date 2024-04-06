using System;
using UnityEngine;

public class Gatherer : MonoBehaviour
{
	public static event Action<IHitTarget> TargetHit;

	public void Hit(IHitTarget hitTarget)
	{
		TargetHit?.Invoke(hitTarget);
	}
}