using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public Transform cameraPivot;
    public float moveSpeed = 5f;
    public float turnSpeed = 8f;
    public float jumpForce = 5f;
    public Ladder ladder;
    public float groundCollisionExtent = 0.5f;
    public bool grounded;
    public bool jumped = false;
    public bool dead = false;
    int floorMask;
    Vector3 lastMove;

    public Ladder Ladder
    {
        get { return ladder; }
        set 
        {
            ladder = value;
            rigidbody.useGravity = ladder == null;
        }
    }
       
    // Use this for initialization
	void Start () {
        floorMask = LayerMask.GetMask("Floor");
        grounded = true;
        lastMove = transform.forward;
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateGrounded();
        Move();
        Rotate();
        Jump();
        CheckKillPlane();
        ResetPosition();
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

    private void Die()
    {
        dead = true;
    }

    public void ResetPosition()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(12.61f, 1.0f, -11.76f);
        }
    }
}
