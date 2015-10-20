using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextWaypoints;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("NO WAYPOINT VAI");
        if (other.gameObject.CompareTag("Barrel"))
        {
            BarrelMovement barril = other.gameObject.GetComponent<BarrelMovement>();
            barril.SearchForOtherWaypoint(this);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("LALALALA");
    }
}