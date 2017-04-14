using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.flavienm.engine;

public class ScrollingCamera : Player {

	public float speed = 1.5f;

	public float yLimit;

	public int numberOflevel;
	public int levelBeforeScroll;
	[SerializeField]
	private int mark;
	private bool lose;

	protected override void OnNewGame()
	{
		lose = false;
		mark = 0;
	}

	protected override void OnMenu()
	{
		base.OnMenu();
		base.OnNewGame();
	}

	protected override void OnGameOver()
	{
		base.OnGameOver();
		lose = true;
	}

	protected override void OnMarkPoint()
	{
		base.OnMarkPoint();
		Debug.Log("Hello");
		mark++;
		if (mark == numberOflevel)
			DispatchWinEvent();
	}

	private void Update () {
		scrollingMovement ();
	}

	private void scrollingMovement(){
		if (mark < levelBeforeScroll)
			return;
		if (transform.position.y > -yLimit && !lose)
		{
			float scrollingSpeed = Time.deltaTime * speed;
			transform.position += new Vector3(0, -scrollingSpeed, 0);
		}
		
	}
}
