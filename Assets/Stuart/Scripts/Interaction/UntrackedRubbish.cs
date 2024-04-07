using System;
using UnityEngine;

public class UntrackedRubbish : MonoBehaviour, IHitTarget
{
	[SerializeField]
	private GameObject model;

	private void Awake()
	{
		if (model == null) model = gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		var gatherer = other.gameObject.GetComponent<Gatherer>();
		if (gatherer != null)
		{
			HandleHit(gatherer);
		}
	}

	private void HandleHit(Gatherer gatherer)
	{
		//gatherer.Hit(this);
		AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.globalSoundList.suck);
		DestroyRubbish();
	}

	private void DestroyRubbish()
	{
		model.SetActive(false);
		// do vfx / coroutine etc
		Destroy(this);
	}
}