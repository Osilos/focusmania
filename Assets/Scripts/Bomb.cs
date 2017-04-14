using com.flavienm.engine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
	public float ExplosionRadius = 8f;
	public float ExplosionDelay = 1f;
	public GameObject ExplosionFX;

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
				GameObject explosionFX = Instantiate(ExplosionFX);
				explosionFX.transform.position = new Vector3(transform.position.x, transform.position.y, -6);
				hit.GetComponent<Bomb>().Hit(true);
			}
			Rigidbody rb = hit.GetComponent<Rigidbody>();

			if(rb != null)
				rb.AddExplosionForce(200f, transform.position, ExplosionRadius);
		}
		colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, -5), ExplosionRadius - 2);
		foreach(Collider hit in colliders)
		{
			if(hit.gameObject.layer == LayerMask.NameToLayer("Destructible"))
			{
				hit.GetComponent<TriangleExplosion>().StartCoroutine(hit.gameObject.GetComponent<TriangleExplosion>().SplitMesh(false));
			}
		}
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		CancelInvoke();
	}
}
