using UnityEngine;
using System.Collections;

public class CameraInput : MonoBehaviour {

    public Transform cameraPivot;
    public float minZ = 0;
    public float maxZ = 90;

    public float ySpeed = 90;
    public float zSpeed = 45;

    //public float rotationSpeed = 90;
    //public Vector3 rotation;

    //public float speed = 55;
    //private float rotation = 0;
    //private Quaternion qTo = Quaternion.identity;

    //public Vector3 rotation;
      

    //public Input m_prevGetAxis;

	// Use this for initialization
	void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
        FollowPlayer();

        
	}

    private void FollowPlayer()
    {
        if (PlayerData.Me.gameObject != null)
        {
            cameraPivot.position = PlayerData.Me.gameObject.transform.position;
        }
    }

    private void Rotate()
    {
        //float rotatio = Input.GetAxis("CameraRotation") * rotationSpeed;
        //rotation *= Time.deltaTime;
        //transform.Rotate(0, rotation, 0);


        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    rotation += 90;
        //    qTo = Quaternion.Euler(0, 0, rotation);
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    rotation -= 90;
        //    qTo = Quaternion.Euler(0, 0, rotation);
        //}

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, speed * Time.deltaTime);


        float h = Input.GetAxis("CameraRotation") * Time.deltaTime;
        Vector3 rotation = cameraPivot.eulerAngles;
        rotation.y = rotation.y + h * ySpeed;

        cameraPivot.eulerAngles = rotation;
    }

    //private void UpDate()
    //{
    //    m_prevGetAxis = Input.GetAxis("CameraRotation");
    //}
}
