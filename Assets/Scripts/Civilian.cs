using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour {

    Vector3 direction;
    private GameObject[] raycastArray = new GameObject[3];
	// Use this for initialization
	void Start () {

        direction = Vector3.right;
        int childrenNumber = transform.childCount;
        Debug.Log(childrenNumber);
        for(int i=0; i < childrenNumber; i++)
        {
            raycastArray[i] = transform.GetChild(i).gameObject;
        }
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(raycastArray[0].transform.position, direction * 100, Color.white);
        Debug.DrawRay(raycastArray[1].transform.position, direction * 100, Color.yellow);
        Debug.DrawRay(raycastArray[2].transform.position, direction * 100, Color.red);
    }

    void FixedUpdate()
    {
        if(DetectionCollision(raycastArray[0].transform.position) || DetectionCollision(raycastArray[1].transform.position) || DetectionCollision(raycastArray[2].transform.position))
        {
            Debug.Log("COLLISION, tu avances pas");
        }
    }

    private bool DetectionCollision(Vector3 origins)
    {
        RaycastHit hit;
        if (Physics.Raycast(origins, direction, out hit, 500f,1 << 8))
            return true;
        else
            return false;
    }

    private void ChangeDirection()
    {
        direction *= -1;
    }
    
}
