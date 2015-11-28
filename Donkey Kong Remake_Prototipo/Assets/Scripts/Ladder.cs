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

    private FireBall_Move GetEnemy (Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FireBall_Move enemy = other.gameObject.GetComponent<FireBall_Move>();
            return enemy;
        }
        return null;
    }


    void OnCollisionEnter(Collision other)
    {
        CharacterMovement player = GetPlayer(other);
        if (player != null) 
        {
            player.EnterLadder(this);
        }

        FireBall_Move enemy = GetEnemy(other);
        if (enemy !=null)
        {
            enemy.EnterLadder(this);
        }
    }

    void OnCollisionExit(Collision other)
    {
        CharacterMovement player = GetPlayer(other);
        if (player != null)
        {
            player.LeaveLadder();
        }

        FireBall_Move enemy = GetEnemy(other);
        if (enemy !=null)
        {
            enemy.LeaveLadder();
        }
    }
}
