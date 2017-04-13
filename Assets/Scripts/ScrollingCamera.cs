using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCamera : MonoBehaviour {

    public float speed = 1.5f;
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scrollingMovement ();
		
	}

	void scrollingMovement(){
        float scrollingSpeed = Time.deltaTime *speed;
        transform.position += new Vector3 (0,-scrollingSpeed, 0);
        
    }
}
