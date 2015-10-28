using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public float moveSpeed = 5f;
    public Transform cameraPivot;
    public int jumpForce = 5;
    public bool ladder;
    public float groundCollisionExtent = 0.5f;
    int floorMask;
    float colliderExtentY;
    bool movedUp;

    public bool Ladder
    {
        get { return ladder; }
        set 
        {
            if (value && ladder != value)
            {
                movedUp = false;
            }
            ladder = value;
            rigidbody.useGravity = !ladder;
        }
    }
       
    // Use this for initialization
	void Start () {
        floorMask = LayerMask.GetMask("Floor");
        colliderExtentY = collider.bounds.extents.y;
        movedUp = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (IsGrounded() || ladder)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (!ladder)
            {
                Vector3 moveDirection = v * cameraPivot.forward;
                moveDirection += h * cameraPivot.right;
                moveDirection *= moveSpeed;
                moveDirection *= Time.deltaTime;
                rigidbody.MovePosition(rigidbody.position + moveDirection);
            }
            else
            {
                Vector3 moveDirection = v * Vector3.up;
                moveDirection *= moveSpeed;
                moveDirection *= Time.deltaTime;
                rigidbody.MovePosition(rigidbody.position + moveDirection);
                rigidbody.velocity = new Vector3(0, 0, 0);

                if (!IsGrounded())
                {
                    movedUp = true;
                }
                else if (movedUp)
                {
                    ladder = false;
                }
            }
        }

        cameraPivot.position = rigidbody.position;

        Jump();
	}

    public bool IsGrounded()
    {
        RaycastHit hit;
        
        return Physics.SphereCast(transform.position, 0.5f, -Vector3.up, out hit, groundCollisionExtent, floorMask);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && !ladder)
        {
            rigidbody.velocity += jumpForce * Vector3.up;
        }
    }

}
