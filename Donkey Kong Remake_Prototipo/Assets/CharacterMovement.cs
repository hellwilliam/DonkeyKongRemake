using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
    public float moveSpeed = 5f;
    public Transform cameraPivot;
    public int jumpForce = 5;

       
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = v * cameraPivot.forward;
        moveDirection += h * cameraPivot.right;
        moveDirection *= moveSpeed;
        moveDirection *= Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + moveDirection);

        cameraPivot.position = rigidbody.position;

        jumpTest();
	}

    void jumpTest ()
    {
        //Criar a lógica básica para o pulo
        //if (Input.GetButtonDown("space"))
        //{
        //    transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);
        //}

        if (Input.GetKeyDown (KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            rigidbody.velocity += jumpForce * Vector3.up;
        }



    }

}
