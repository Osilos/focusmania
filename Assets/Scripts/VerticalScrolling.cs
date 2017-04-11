using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrolling : MonoBehaviour {

    [SerializeField]
    private GameObject GamePlane;
    [SerializeField]
    private GameObject Background;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Background.transform.position += new Vector3(0,  Time.deltaTime * 0.7f, 0);
        GamePlane.transform.position += new Vector3(GamePlane.transform.position.x,  Time.deltaTime, GamePlane.transform.position.z);
		
	}
}
