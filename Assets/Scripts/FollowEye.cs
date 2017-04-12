using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.flavienm.engine;
using com.flavienm.engine.input;

public class FollowEye : MonoBehaviour {

	public static bool laser = false;

	void Start () {
		com.flavienm.engine.input.Input.positionInput += OnMovement;
		com.flavienm.engine.input.Input.space += OnSpace;
	}

	private void OnSpace ()
	{
		laser = !laser;
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
}