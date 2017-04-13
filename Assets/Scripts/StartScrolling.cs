using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScrolling : MonoBehaviour {

    [SerializeField]
    private GameObject mainCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("TA MERE LE SCRIPT");
        mainCamera.GetComponent<ScrollingCamera>().enabled = true;
    }
}
