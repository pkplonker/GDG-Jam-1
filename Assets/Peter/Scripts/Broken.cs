using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broken : MonoBehaviour
{
	[SerializeField]
	float impactForce = 5;

	void Start()
	{
		Rigidbody rb = GetComponent<Rigidbody>();

		Vector3 force = new Vector3(Random.Range(-impactForce, impactForce), Random.Range(-impactForce, impactForce),
			Random.Range(-impactForce, impactForce));

		rb.AddForce(force, ForceMode.Impulse);
	}
}