using UnityEngine;
using System.Collections;

public class CameraInput : MonoBehaviour {

    public Transform cameraPivot;
    public Transform player;
    public float minZ = 0;
    public float maxZ = 90;

    public float ySpeed = 90;
    public float zSpeed = 45;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
        FollowPlayer();
	}

    private void FollowPlayer()
    {
        cameraPivot.position = player.position;
    }

    private void Rotate()
    {
        float h = Input.GetAxis("CameraRotation") * Time.deltaTime;
        Vector3 rotation = cameraPivot.eulerAngles;
        rotation.y = rotation.y + h * ySpeed;
        cameraPivot.eulerAngles = rotation;
    }
}
