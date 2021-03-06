using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barrel : MonoBehaviour 
{
    public static bool moving;
    public Waypoint waypointToFollow;
    public float rotationSpeed = 10f;
    public float speed = 10f;
    public bool isStatic = false;
    bool dead;

    private Waypoint lastWaypoint;
    void OnTriggerEnter(Collider other)
    {
        Waypoint waypoint = other.gameObject.GetComponent<Waypoint>();
        if (waypoint != null && waypoint != lastWaypoint && (waypoint == waypointToFollow || waypointToFollow == null))
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
        }
        else
        {
            waypointToFollow = null;
            if (!isStatic)
            {
                Despawn();
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
            player.Die();
        }
    }

    void Burn()
    {
        dead = true;
        collider.enabled = false;
        Despawn();
    }

    void Despawn(float delay = 1f)
    {
        if (Network.isServer || networkView.isMine)
        {
            Network.Destroy(gameObject);
        }
        else if (Network.peerType == NetworkPeerType.Disconnected)
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        moving = true;
    }

    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving && (networkView.isMine || Network.peerType == NetworkPeerType.Disconnected))
        {
            if (waypointToFollow != null)
            {
                Vector3 pos = waypointToFollow.transform.position - rigidbody.position;
                pos.y = 0; // force rotation on Y
                Quaternion myRot = Quaternion.LookRotation(pos);
                //rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, myRot, rotationSpeed * Time.deltaTime);
                rigidbody.MoveRotation(Quaternion.Slerp(rigidbody.rotation, myRot, rotationSpeed * Time.deltaTime));

                Vector3 force = transform.forward * (Time.deltaTime * speed);
                Vector3 newPosition = rigidbody.position + force;
                rigidbody.position = newPosition;
            }

            if (dead)
            {
                rigidbody.position = rigidbody.position + ((-transform.up) * speed * Time.deltaTime);
            }

            if (rigidbody.position.y < -10)
            {
                Despawn(0);
            }
        }
    }
}
