using UnityEngine;
using System.Collections;

public class NewScene : MonoBehaviour
{    
    public void LoadScene (string level)
    {
         Application.LoadLevel(level);
    }

	// Use this for initialization
    //void Start () 
    //{
	
    //}
	
    //Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
