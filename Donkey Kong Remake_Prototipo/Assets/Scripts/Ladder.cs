using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    public float impulseForwardForce = 0.5f;
    public float impulseUpForce = 2f;
    CharacterMovement player;
    bool moved;

    private void TriggerLadder(Collision other, Ladder ladder)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Ladder = ladder;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        TriggerLadder(other, this);
        moved = false;
    }

    void OnCollisionExit(Collision other)
    {
        TriggerLadder(other, null);
        if (moved && !player.grounded)
        {
            Vector3 impulseForward = transform.forward * impulseForwardForce;
            Vector3 impulseUp = Vector3.up * impulseUpForce;
            player.rigidbody.AddForce(impulseForward + impulseUp, ForceMode.Impulse); // unlatching from the top
        }
    }

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        moved = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if (player.Ladder == this)
        //{
        //    player.rigidbody.velocity = new Vector3(0, 0, 0);
        //    if (!moved && !player.grounded)
        //    {
        //        moved = true;
        //    }
        //    else if (moved && player.grounded)
        //    {
        //        player.Ladder = null; // unlatching from the bottom, not exiting collision
        //    }
        //}
	}
}
