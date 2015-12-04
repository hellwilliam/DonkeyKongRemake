using UnityEngine;
using System.Collections;

public class FireBall_Move : MonoBehaviour 
{
    //My Code-------------------------------------------------------------------------
    //public float stepSpeed = 2f;
    //public Transform moveFireBall;
    //public Vector3 moveFoward = Vector3.zero;
    public NavMeshAgent nav;
    public Transform target;
    public Ladder ladder;
    public bool ladderStartedClimb;
    public bool grounded;

    public float lifeSpan = 30f;
    //public Player player;


    //void Speed()
    //{
    //    moveFoward.z = 1 * Time.deltaTime;
    //}

    //void playerToFollow(Transform other)
    //{
    //    if (other.gameObject.CompareTag("player"))
    //    {
    //        CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
    //    }
    //}

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
        
   
        //Speed();
        //moveFireBall.rigidbody.AddRelativeForce(moveFoward * 20, ForceMode.VelocityChange);
    }
    //--------------------------------------------------------------------------------

    //public Transform stuff;
    //public Vector3 vel;
    //public float swithDirectrion = 3f;
    //public float curTime = 0;

    //// Use this for initialization
    //void Start () 
    //{
    //    SetVel();	
    //}

    //private void SetVel()
    //{
    //    if (Random.value > .5)
    //    {
    //        vel.x = 4 * 4 * Random.value;
    //    }
    //    else
    //    {
    //        vel.x = -4 * 4 * Random.value;
    //    }

    //    if (Random.value > .5)
    //    {
    //        vel.z = 4 * 4 * Random.value;
    //    }

    //    else
    //    {
    //        vel.z = -4 * 4 * Random.value;
    //    }
    //}
	
    //// Update is called once per frame
    //void Update ()
    //{
    //    if (curTime < swithDirectrion)
    //    {
    //        curTime += 1 * Time.deltaTime;
    //    }

    //    else
    //    {
    //        SetVel();
    //        if (Random.value > 5)
    //        {
    //            swithDirectrion += Random.value;
    //        }

    //        else
    //        {
    //            swithDirectrion -= Random.value;
    //        }
    //    }

    //    if (swithDirectrion < 1)
    //    {
    //        swithDirectrion = 1 + Random.value;
    //    }

    //    curTime = 0;
    //}

    //void FixedUpdate()
    //{
    //    stuff.rigidbody.velocity = vel;
    //}
}
