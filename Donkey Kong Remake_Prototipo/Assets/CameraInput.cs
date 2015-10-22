using UnityEngine;
using System.Collections;

public class CameraInput : MonoBehaviour {

    public Transform cameraPivot;
    public float minZ = 0;
    public float maxZ = 90;

    public float ySpeed = 90;
    public float zSpeed = 45;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Mouse X") * (Input.GetMouseButton(0) ? 1 : 0) * Time.deltaTime;
        float v = Input.GetAxis("Mouse Y") * (Input.GetMouseButton(0) ? 1 : 0) * -1 * Time.deltaTime;
        Vector3 rotation = cameraPivot.eulerAngles;
        rotation.z = Mathf.Clamp(rotation.z + v * zSpeed, minZ, maxZ);
        rotation.y = rotation.y + h * ySpeed;
        rotation.x = 0f;
        cameraPivot.eulerAngles = rotation;
	}
}
