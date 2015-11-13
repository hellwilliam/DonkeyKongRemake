using UnityEngine;
using System.Collections;

public class FireBall_Move : MonoBehaviour 
{
    public Transform stuff;
    public Vector3 vel;
    public float swithDirectrion = 3f;
    public float curTime = 0;

	// Use this for initialization
	void Start () 
    {
        SetVel();	
	}

    private void SetVel()
    {
        if (Random.value > .5)
        {
            vel.x = 4 * 4 * Random.value;
        }
        else
        {
            vel.x = -4 * 4 * Random.value;
        }

        if (Random.value > .5)
        {
            vel.z = 4 * 4 * Random.value;
        }

        else
        {
            vel.z = -4 * 4 * Random.value;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (curTime < swithDirectrion)
        {
            curTime += 1 * Time.deltaTime;
        }

        else
        {
            SetVel();
            if (Random.value > 5)
            {
                swithDirectrion += Random.value;
            }

            else
            {
                swithDirectrion -= Random.value;
            }
        }

        if (swithDirectrion < 1)
        {
            swithDirectrion = 1 + Random.value;
        }

        curTime = 0;
  	}

    void FixedUpdate()
    {
        stuff.rigidbody.velocity = vel;
    }
}
