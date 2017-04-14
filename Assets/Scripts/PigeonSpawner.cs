using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonSpawner : MonoBehaviour {

	public GameObject GOPigeon;

	private Coroutine currentCoroutine = null;

	private void Update()
	{
		if (currentCoroutine == null)
		{
			currentCoroutine = StartCoroutine(LaunchPigeon());
		}
	}

	IEnumerator LaunchPigeon()
	{
		yield return new WaitForSeconds(5 + Random.Range(0f, 4f));
		Debug.Log("yolo");
		GameObject pigeon = Instantiate(GOPigeon);
		pigeon.transform.position = transform.position + new Vector3(0, Random.Range(-GetComponent<Renderer>().bounds.size.y / 2, GetComponent<Renderer>().bounds.size.y / 2), 0);
		currentCoroutine = null;
	}
}

