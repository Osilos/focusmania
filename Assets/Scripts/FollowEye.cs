using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.flavienm.engine;
using com.flavienm.engine.input;
using GAF.Core;

public class FollowEye : Player {

	public static bool laser = false;

	public ParticleSystem LaserParticle;
	public ParticleSystem SmokeParticle;

	private Coroutine currentCoroutine;

	public bool isPlaying;
	[SerializeField]
	private AudioSource laserSound;
	[SerializeField]
	private AudioSource xRaySound;

	void Start () {
		com.flavienm.engine.input.Input.positionInput += OnMovement;
		com.flavienm.engine.input.Input.space += OnSpace;
		GameManager.NewGame += SwitchState;
		LaserParticle.gameObject.SetActive(false);
		StartCoroutine(ReActivateCollider());
	}

	void OnDestroy ()
	{
		com.flavienm.engine.input.Input.positionInput -= OnMovement;
		com.flavienm.engine.input.Input.space -= OnSpace;
		GameManager.NewGame -= SwitchState;

	}

	protected override void OnNewGame()
	{
	}

	protected override void OnMenu()
	{
		base.OnMenu();
		isPlaying = false;
		laser = false;
		LaserParticle.gameObject.SetActive(false);
	}

	private void OnSpace ()
	{
		laser = !laser;
		
		if (laser)
		{
			laserSound.Play();
			xRaySound.Stop();
			LaserParticle.gameObject.SetActive(true);
			StartCoroutine(ReActivateCollider());
		}
		else
		{
			laserSound.Stop();
			xRaySound.Play();
			xRaySound.SetScheduledEndTime(AudioSettings.dspTime + (5));
			LaserParticle.gameObject.SetActive(false);
		}
	}

	IEnumerator ReActivateCollider ()
	{
		GetComponent<Collider>().enabled = false;
		yield return new WaitForSeconds(0.05f);
		GetComponent<Collider>().enabled = true;
	}

	private void OnMovement (Vector3 position)
	{
		position.z = 0f;
		transform.position = position;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!isPlaying)
		{
			return;
		}
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
				StartCoroutine(other.gameObject.GetComponent<TriangleExplosion>().SplitMesh(false));
			}

			if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
			{
				OnLose();
				other.GetComponent<Bomb>().Hit(false);
			}

			if(other.CompareTag("Civil"))
			{
				if(other.GetComponent<GAFMovieClip>().currentSequence.name != "hit")
				{
					other.GetComponent<GAFMovieClip>().setSequence("hit", true);
					other.GetComponent<Civilian>().cri(true);
				}
			}

			if(other.CompareTag("Pigeon"))
			{
				if(other.GetComponent<GAFMovieClip>().currentSequence.name != "Pigeon_vole")
				{
					other.GetComponent<GAFMovieClip>().setSequence("Pigeon_mort", true);
					Vector3 explosionPos = new Vector3(other.transform.position.x + Random.Range(-0.5f, 0.5f), other.transform.position.y + Random.Range(0f, 0.5f), other.transform.position.z + Random.Range(-0.5f, 0.5f));
					other.gameObject.AddComponent<Rigidbody>().AddExplosionForce(Random.Range(300, 500), explosionPos, 5);
					other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
					Destroy(other.gameObject, 5f);
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Civil"))
		{
			if(other.GetComponent<GAFMovieClip>().currentSequence.name == "hit")
			{
				other.GetComponent<GAFMovieClip>().setSequence("attente", true);
				other.GetComponent<Civilian>().cri(false);
			}
		}
	}

	public IEnumerator LaunchSmokeLaser()
	{
		yield return new WaitForSeconds(0.06f);
		SmokeParticle.Stop(false);
		currentCoroutine = null;
	}

	public void SwitchState()
	{
		StartCoroutine(ReActivateCollider());
		isPlaying = !isPlaying;
	}
}