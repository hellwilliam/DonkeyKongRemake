using UnityEngine;
using System.Collections;

public class ScoreZone : MonoBehaviour {
    bool scored;
    public int points = 300;
	// Use this for initialization
	void Start () {
        scored = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && !scored)
        {
            scored = true;
            Debug.Log("SCORE");
        }
    }
}
