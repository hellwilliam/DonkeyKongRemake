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
    int floorMask;
    Vector3 lastMove;

    public Ladder Ladder
    {
        get { return ladder; }
        set 
        {
            ladder = value;
            rigidbody.useGravity = ladder == null;
            if (ladder != null)
            {
                lastMove = ladder.transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }
       
    // Use this for initialization
	void Start () {
        floorMask = LayerMask.GetMask("Floor");
        grounded = true;
        lastMove = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
    {
        UpdateGrounded();
        Move();
        Rotate();
        Jump();
	}

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = Vector3.zero;
        
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
            if (moveDirection.magnitude != 0)
            {
                lastMove = moveDirection;
            }
            transform.Translate(moveDirection, Space.World);
        }
        else
        {
            transform.Translate(lastMove, Space.World);
        }
    }

    private void Rotate()
    {
        Quaternion lookRotation = Quaternion.LookRotation(lastMove, Vector3.up);
        lookRotation.x = 0;
        lookRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        //Vector3 lookTowards = (transform.position + lastMove);
        //lookTowards.y = transform.position.y;
        //transform.LookAt(lookTowards);
    }

    private void UpdateGrounded()
    {
        RaycastHit hit;
        grounded = Physics.SphereCast(transform.position, 0.5f, -Vector3.up, out hit, groundCollisionExtent, floorMask);
    }

   private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded && ladder == null)
        {
            rigidbody.velocity += jumpForce * Vector3.up;
        }
    }

}
