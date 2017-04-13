using com.flavienm.engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public float ExplosionRadius = 8f;
	public float ExplosionDelay = 1f;

	private void Start()
	{
		GameManager.BlowBombs += LaunchEndExplosion;
	}

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

	public void LaunchEndExplosion()
	{
		StartCoroutine(EndExplosion());
	}

	IEnumerator EndExplosion()
	{
		yield return new WaitForSeconds(Random.Range(0.1f, 0.9f));
		Explosion();
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

	private void OnDestroy()
	{
		CancelInvoke();
	}
}
