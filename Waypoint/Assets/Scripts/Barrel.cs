using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour 
{
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("AEAOSEHJAOSEASJKLHEAJKSE");
        Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();
        if (waypoint != null)
        {
            ChangeTarget(waypoint);

        }

    }

    private void ChangeTarget(Waypoint waypoint)
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("LALALALA2");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
