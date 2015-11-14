using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static GameObject me;
    public GameObject player1prefab;
    public GameObject player2prefab;

	// Use this for initialization
	void Start () {
	
	}

    void DoThings()
    {
        
        if (Network.peerType == NetworkPeerType.Client)
        {
            me = (GameObject)Network.Instantiate(player1prefab, new Vector3(9.5f, 1, -12), Quaternion.identity, 0);
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            me = (GameObject)Network.Instantiate(player2prefab, new Vector3(7.5f, 1, -12), Quaternion.identity, 0);
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
