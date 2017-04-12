using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPart : MonoBehaviour {

	[SerializeField]
	private Material[] materials;

	[SerializeField]
	private float distanceToPlayer;
	[SerializeField]
	private string playerName;

	private Transform playerTransform;

	private Renderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer>();
		playerTransform = GameObject.Find(playerName).transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector2.Distance(playerTransform.position, transform.position) > distanceToPlayer || FollowEye.laser)
		{
		   render.material = materials[1];
		}
		else
		{
			render.material = materials[0];
		}
	}
}
