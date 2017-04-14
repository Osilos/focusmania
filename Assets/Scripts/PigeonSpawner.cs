using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSpawner : MonoBehaviour {

	public GameObject GOPigeon;

	private Coroutine currentCoroutine;

	private void Start()
	{
		if (currentCoroutine != null)
		{
			currentCoroutine = StartCoroutine(LaunchPigeon());
		}
	}

	IEnumerator LaunchPigeon()
	{
		yield return new WaitForSeconds(5 + Random.Range(0f, 4f));
		GameObject pigeon = Instantiate(GOPigeon);
		pigeon.transform.position = transform.position + new Vector3(0, Random.Range(-GetComponent<Collider>().bounds.size.y / 2, GetComponent<Collider>().bounds.size.y / 2), 0);
	}
}

