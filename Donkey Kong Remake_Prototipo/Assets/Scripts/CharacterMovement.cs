using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public Transform cameraPivot;
    public float moveSpeed = 5f;
    public float turnSpeed = 8f;
    public float jumpForce = 5f;
    public Ladder ladder;
    public bool ladderStartedClimb;
    public float groundCollisionExtent = 0.5f;
    public bool grounded;
    public bool jumped;
    public bool dead;
    public bool won;
    int floorMask;
    Vector3 lastMove;

    public void EnterLadder(Ladder ladder)
    {
        this.ladder = ladder;
        rigidbody.useGravity = false;
        rigidbody.velocity = new Vector3(0, 0, 0);
        ladderStartedClimb = false;
    }

    public void LeaveLadder()
    {
        rigidbody.useGravity = true;
        if (!grounded)
        {
            Vector3 impulseForward = transform.forward * ladder.impulseForwardForce;
            Vector3 impulseUp = Vector3.up * ladder.impulseUpForce;
            rigidbody.AddForce(impulseForward + impulseUp, ForceMode.Impulse); // unlatching from the top
        }
        ladder = null;
    }
       
    // Use this for initialization
	void Awake () {
        cameraPivot = GameObject.Find("CameraPivot").transform;
        floorMask = LayerMask.GetMask("Floor");
        grounded = true;
        jumped = false;
        dead = false;
        won = false;
        ladderStartedClimb = false;
        lastMove = transform.forward;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!dead && !won)
        {
            if (networkView.isMine)
            {
                UpdateGrounded();
                Move();
                Rotate();
                Jump();
                CheckKillPlane();
            }
        }
        CheckRestartInput();
	}

    private void CheckRestartInput()
    {
        if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Restart"))
        {
            if (dead)
            {
                GameOverManager.instance.Restart();
            }
            else if (won)
            {
                VictoryManager.instance.Restart();
            }
        }
    }

    private void CheckKillPlane()
    {
        if (transform.position.y < -10)
        {
            Die();
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward;
        if (ladder)
        {
            // let ladder control the user ?
            // how to make it so that we can control it with left/right depending on where the ladder is relative to the camera/player?
            moveDirection = v * Vector3.up;
            moveDirection *= moveSpeed;
            moveDirection *= Time.deltaTime;
            transform.Translate(moveDirection, Space.World);

            if (!ladderStartedClimb && !grounded)
            {
                ladderStartedClimb = true;
            }
            else if (ladderStartedClimb && grounded)
            {
                LeaveLadder();
            }
        }
        else if (grounded)
        {
            moveDirection = v * cameraPivot.forward;
            moveDirection += h * cameraPivot.right;
            moveDirection *= moveSpeed;
            moveDirection *= Time.deltaTime;
            lastMove = moveDirection;
            transform.Translate(moveDirection, Space.World);
        }
        else if (jumped)
        {
            transform.Translate(lastMove, Space.World);
        }
    }

    private void Rotate()
    {
        Quaternion lookRotation = transform.rotation;

        if (!ladder)
        {
            Vector3 orientation = lastMove;
            if (orientation.magnitude == 0f)
            {
                orientation = transform.forward;
            }
            lookRotation = Quaternion.LookRotation(orientation, transform.up);
        }
        else
        {
            Vector3 playerToLadder = ladder.transform.position - transform.position;
            playerToLadder.y = 0;
            lookRotation = Quaternion.LookRotation(playerToLadder, transform.up);
        }

        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime); // we could use raycasting to look perpendicular to the object, but nah
    }

    private void UpdateGrounded()
    {
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, 0.5f, -Vector3.up, out hit, groundCollisionExtent, floorMask);
        if (jumped)
        {
            jumped = !grounded;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded && ladder == null)
        {
            rigidbody.velocity += jumpForce * Vector3.up;
            jumped = true;
        }
    }

    public void Die()
    {
        if (networkView.isMine)
        {
            dead = true;
            GameOverManager.instance.GameOver();
        }
    }

    public void Win()
    {
        if (networkView.isMine)
        {
            won = true;
            VictoryManager.instance.Win();
        }
    }
}
