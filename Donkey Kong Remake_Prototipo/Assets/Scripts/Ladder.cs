using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
    public float impulseForwardForce = 0.5f;
    public float impulseUpForce = 2f;

    private CharacterMovement GetPlayer(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CharacterMovement player = other.gameObject.GetComponent<CharacterMovement>();
            return player;
        }
        return null;
    }

    void OnCollisionEnter(Collision other)
    {
        CharacterMovement player = GetPlayer(other);
        player.EnterLadder(this);
    }

    void OnCollisionExit(Collision other)
    {
        CharacterMovement player = GetPlayer(other);
        player.LeaveLadder();
    }
}
