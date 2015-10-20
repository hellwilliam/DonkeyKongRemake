using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrel : MonoBehaviour 
{
    public Waypoint waypointToFollow;
    private Waypoint lastWaypoint;
    void OnTriggerEnter(Collider other)
    {
        Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();
        if (waypoint != null && waypoint != lastWaypoint)
        {
            ChangeTarget(waypoint);
        }
    }

    void ChangeTarget(Waypoint waypoint)
    {
        lastWaypoint = waypoint;
        if (waypoint.nextWaypoints.Count > 0)
        {
            waypoint.lastChoice = (waypoint.lastChoice + 1) % waypoint.nextWaypoints.Count;
            waypointToFollow = waypoint.nextWaypoints[waypoint.lastChoice];
            Vector3 waypointToFollowPosition = waypointToFollow.transform.position;
            transform.LookAt(new Vector3(waypointToFollowPosition.x, transform.position.y, waypointToFollowPosition.z));
        }
        else
        {
            waypointToFollow = null;
        }
    }

    public float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (waypointToFollow != null)
        {
            Vector3 force = transform.forward * (Time.deltaTime * speed);
            Vector3 newPosition = transform.position + force;
            transform.position = newPosition;
        }
    }
}
