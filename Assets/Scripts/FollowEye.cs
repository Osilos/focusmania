using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.flavienm.engine;

public class FollowEye : MonoBehaviour {

	// Use this for initialization
	void Start () {
		com.flavienm.engine.input.Input.positionInput += OnMovement;
	}
	
	private void OnMovement (Vector3 position)
	{
        position.z = 0f;
		transform.position = position;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
