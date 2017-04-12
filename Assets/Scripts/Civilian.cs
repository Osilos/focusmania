using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{

    Vector3 direction;
    private GameObject[] raycastArray = new GameObject[3];
    [SerializeField]
    private List<Transform> startPoints = new List<Transform>();
    [SerializeField]
    private List<Transform> endPoints = new List<Transform>();
    [SerializeField]
    private float speed;
    // Use this for initialization
    void Start()
    {

        direction = Vector3.right;
        int childrenNumber = transform.childCount;
        Debug.Log(childrenNumber);
        for (int i = 0; i < childrenNumber; i++)
        {
            raycastArray[i] = transform.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(raycastArray[0].transform.position, direction * 100f, Color.white);
        Debug.DrawRay(raycastArray[1].transform.position, direction * 100f, Color.yellow);
        Debug.DrawRay(raycastArray[2].transform.position, direction * 100f, Color.red);
    }

    void FixedUpdate()
    {
        if (!DetectionCollision(raycastArray[0].transform.position) && !DetectionCollision(raycastArray[1].transform.position) && !DetectionCollision(raycastArray[2].transform.position))
        {
            Debug.Log("COLLISION, tu avances pas");
            transform.position += new Vector3(direction.x * Time.deltaTime * speed, 0f, 0f);
        }
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
        Debug.Log("trigger enter");
        if (other.gameObject.tag == "Exit")
        {
            Debug.Log("ouloulou l'escalier");
            startPoints.RemoveAt(0);
            transform.position = startPoints[0].position;
        }
    }
}
