using UnityEngine;
using System.Collections;

public class BarrelMovement : MonoBehaviour
{
    public int speed = 100;
    Vector3 direction = Vector3.forward;
    Vector3 movePosition = Vector3.zero;
    Vector3 newPosition = Vector3.zero;



    public void SearchForOtherWaypoint(Waypoint waypoint)
    {
        //waypoint.next
    }
    void wake()
    {

    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = direction * (speed * Time.deltaTime);
        newPosition = transform.position + newPosition;
    }
}
