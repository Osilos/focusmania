using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.flavienm.engine;
using GAF;
using GAF.Core;

public class Civilian : Player
{
	private Vector3 direction;
	[SerializeField]
	private GameObject[] raycastArray;
	[SerializeField]
	private List<Transform> startPoints = new List<Transform>();

	[SerializeField]
	private float speed;

	private bool isPlaying;

	private int mark;

	private void Start()
	{
		direction = Vector3.right;
	}
	
	private void Update()
	{
		Debug.DrawRay(raycastArray[0].transform.position, direction * 100f, Color.white);
		Debug.DrawRay(raycastArray[1].transform.position, direction * 100f, Color.yellow);
		Debug.DrawRay(raycastArray[2].transform.position, direction * 100f, Color.red);
	}

	private void FixedUpdate()
	{
		if (!isPlaying)
			return;
		if (!DetectionCollision(raycastArray[0].transform.position) 
			&& !DetectionCollision(raycastArray[1].transform.position) 
			&& !DetectionCollision(raycastArray[2].transform.position))
		{
			if (GetComponent<GAFMovieClip>().currentSequence.name != "course")
			{
				GetComponent<GAFMovieClip>().setSequence("course", true);
			}
			transform.position += new Vector3(direction.x * Time.deltaTime * speed, 0f, 0f);
		} else
		{
			if(GetComponent<GAFMovieClip>().currentSequence.name != "attente")
			{
				GetComponent<GAFMovieClip>().setSequence("attente", true);
			}
		}
	}

	protected override void OnNewGame()
	{
		isPlaying = true;
		mark = 0;
	}

	protected override void OnMenu()
	{
		base.OnMenu();
		base.OnNewGame();
		isPlaying = false;
	}

	public void SwitchState()
	{
		isPlaying = !isPlaying;
	}

	private bool DetectionCollision(Vector3 origins)
	{
		RaycastHit hit;
		if (Physics.Raycast(origins, direction, out hit, 100f, 1 << 8))
			return true;
		else
			return false;
	}

	private void ChangeDirection()
	{
		direction *= -1;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Exit")
		{
			if(startPoints.Count != 0)
			{
				OnMark();
				transform.position = startPoints[mark].position;
				mark++;
			}
		}
	}
}
