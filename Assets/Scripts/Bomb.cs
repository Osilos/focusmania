using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public float ExplosionRadius = 8f;
	public float ExplosionDelay = 1f;

	public void Hit(bool delay)
	{
		if (delay)
		{

			Invoke("Explosion", 1f);
		} else
		{
			Explosion();
		}
	}

	public void Explosion()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
		foreach(Collider hit in colliders)
		{
			if(LayerMask.NameToLayer("Bomb") == hit.gameObject.layer)
			{
				hit.GetComponent<Bomb>().Hit(true);
			}
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if(rb != null)
				rb.AddExplosionForce(200f, transform.position, ExplosionRadius);
		}
		Destroy(gameObject);
	}

	protected void ChangePumped(bool reset)
	{
		GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, 1 - (0.2f * countPumped), 1 - (0.2f * countPumped), GetComponent<SpriteRenderer>().color.a);

		if(reset)
		{
			GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, GetComponent<SpriteRenderer>().color.a);
		}
	}

	private void OnDestroy()
	{
		CancelInvoke();
	}
}
