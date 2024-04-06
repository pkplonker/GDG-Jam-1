using UnityEngine;

public class Rubbish : MonoBehaviour, IHitTarget
{
	[SerializeField]
	private GameObject model;

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
		gatherer.Hit(this);
		DestroyRubbish();
	}

	private void DestroyRubbish()
	{
		model.SetActive(false);
		// do vfx / coroutine etc
		Destroy(this);
	}
}