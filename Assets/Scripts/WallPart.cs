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

	private Transform cameraTransform;

	bool material1;
	bool material0;
    [SerializeField]    
    private float offSetToDestroy;

    // Use this for initialization
    void Start () {
		render = GetComponent<Renderer>();
		playerTransform = GameObject.Find(playerName).transform;
		cameraTransform = Camera.main.transform;
		material0 = true;
		material1 = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y - cameraTransform.position.y > offSetToDestroy)
			gameObject.SetActive(false);

		if ((Vector2.Distance(playerTransform.position, transform.position) > distanceToPlayer || FollowEye.laser))
		{
			if (material1)
				return;
			render.material = materials[1];
			material0 = false;
			material1 = true;
		}
		else if (!material0)
		{
			render.material = materials[0];
			material0 = true;
			material1 = false;
		}
	}
}
