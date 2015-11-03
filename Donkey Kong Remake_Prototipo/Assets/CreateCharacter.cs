using UnityEngine;
using System.Collections;

public class CreateCharacter : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

	// Use this for initialization
	void Start () {
	
	}

    void DoThings()
    {
        if (Network.peerType == NetworkPeerType.Client)
        {
            Network.Instantiate(player1, new Vector3(9.5f, 1, -12), Quaternion.identity, 0);
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            Network.Instantiate(player2, new Vector3(7.5f, 1, -12), Quaternion.identity, 0);
        }
        else
        {
            Debug.Log("NOT CONNECTED");
        }
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.I))
        {
            DoThings();
        }
	}
}
