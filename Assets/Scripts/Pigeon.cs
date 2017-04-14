using GAF.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<GAFMovieClip>().currentSequence.name == "Pigeon_mort")
		{
			return;
		}
		transform.position = new Vector3(transform.position.x + 10, transform.position.y, transform.position.z);
	}
}
