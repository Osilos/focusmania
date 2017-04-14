using GAF.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour {

	// Update is called once per frame

	private void Start()
	{
		Destroy(gameObject, 20f);
	}

	void Update () {
		if (GetComponent<GAFMovieClip>().currentSequence.name == "Pigeon_mort")
		{
			return;
		}
		transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
	}
}
