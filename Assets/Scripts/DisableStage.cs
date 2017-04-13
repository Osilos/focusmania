using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableStage : MonoBehaviour {

	private Transform cameraTransform;

	private void Start ()
	{
		cameraTransform = Camera.main.transform;
	}

	void Update () {
		if (cameraTransform.position.y - transform.position.y < -25f)
			Destroy(gameObject);
	}
}
