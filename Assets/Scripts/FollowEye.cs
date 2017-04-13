using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.flavienm.engine;
using com.flavienm.engine.input;

public class FollowEye : MonoBehaviour {

	public static bool laser = false;

	public ParticleSystem LaserParticle;
	public ParticleSystem SmokeParticle;

	private Coroutine currentCoroutine;

	void Start () {
		com.flavienm.engine.input.Input.positionInput += OnMovement;
		com.flavienm.engine.input.Input.space += OnSpace;
		LaserParticle.gameObject.SetActive(false);
	}

	private void OnSpace ()
	{
		laser = !laser;
		if(laser)
		{
			LaserParticle.gameObject.SetActive(true);
		}
		else
		{
			LaserParticle.gameObject.SetActive(false);
		}
	}
	
	private void OnMovement (Vector3 position)
	{
		position.z = 0f;
		transform.position = position;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (laser)
		{
			if (currentCoroutine != null)
			{
				StopCoroutine(currentCoroutine);
			} else
			{
				SmokeParticle.Play();
			}
			currentCoroutine = StartCoroutine(LaunchSmokeLaser());
			if (other.gameObject.layer == LayerMask.NameToLayer("Destructible"))
			{
				StartCoroutine(other.gameObject.GetComponent<TriangleExplosion>().SplitMesh(true));
			}

			if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
			{
				other.GetComponent<Bomb>().Hit(false);
			}
		}
	}

	public IEnumerator LaunchSmokeLaser()
	{
		yield return new WaitForSeconds(0.06f);
		SmokeParticle.Stop(false);
		currentCoroutine = null;
	}
}