using UnityEngine;
using System.Collections;

public class FireBall_Move : MonoBehaviour 
{    
    public NavMeshAgent nav;
    public Transform target;
    public Ladder ladder;
    public bool ladderStartedClimb;
    public bool grounded;
  
    public float lifeSpan = 30f;
    
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collide with player");
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
            player.Die();
        }
    }

    public void EnterLadder(Ladder ladder)
    {
        Debug.Log ("Entering");
        this.ladder = ladder;
        rigidbody.useGravity = false;
        rigidbody.velocity = new Vector3 (0, 0, 0);
        ladderStartedClimb = false;
    }

    public void LeaveLadder ()
    {
        Debug.Log ("Leaving");
        rigidbody.useGravity = true;
        if (!grounded)
        {
            Vector3 impulseForward = transform.forward * ladder.impulseForwardForce;
            Vector3 impulseUp = Vector3.up * ladder.impulseUpForce;
            rigidbody.AddForce (impulseForward + impulseUp, ForceMode.Impulse);
        }
        ladder = null;
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();

        Destroy(gameObject, lifeSpan);

    }
    
    void Update ()
    {
        if (networkView.isMine || Network.peerType == NetworkPeerType.Disconnected)
        {
            GameObject g = GameObject.FindGameObjectWithTag("DonkeyKong");
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                target = p.transform;
                nav.SetDestination(target.position);
            }

            else if (g != null)
            {
                target = g.transform;
                nav.SetDestination(target.position);
            }
        }
    }

    //GameObject FindClosestPlayer()
    //{       
    //    GameObject[] gos;
    //    gos = GameObject.FindGameObjectsWithTag("Player");
    //    GameObject closest = null;
    //    float distance = Mathf.Infinity;
    //    Vector3 position = transform.position;
    //    foreach (GameObject go in gos)
    //    {
    //        Vector3 diff = go.transform.position - position;
    //        float curDistance = diff.sqrMagnitude;
    //        if (curDistance < distance)
    //        {
    //            closest = go;
    //            distance = curDistance;
               
    //        }
    //    }
    //    return closest;
    //}
}
