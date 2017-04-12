using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public void Explode()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
		foreach(Collider hit in colliders)
		{
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if(rb != null)
				rb.AddExplosionForce(200f, transform.position, 10f);
		}
		Destroy(gameObject);
		
	}
}
