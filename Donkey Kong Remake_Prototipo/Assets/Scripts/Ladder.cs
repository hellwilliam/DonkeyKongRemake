using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    CharacterMovement player;

    private void TriggerLadder(Collision other, bool ladder)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Ladder = ladder;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        TriggerLadder(other, true);
    }

    void OnCollisionExit(Collision other)
    {
        TriggerLadder(other, false);
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
