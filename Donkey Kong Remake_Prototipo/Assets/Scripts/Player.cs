using UnityEngine;
using System.Collections;

public class PlayerData
{
    private static PlayerData me;
    public static PlayerData Me 
    {
        get 
        {
            if (me == null)
            {
                me = new PlayerData();
            }
            return me;
        }
    }
    public string name;
    public Color color;
    public bool ready;
    public GameObject prefab;
    public GameObject gameObject;
    public NetworkPlayer player;
}

public class Player : MonoBehaviour 
{
    
    public GameObject player1prefab;
    public GameObject player2prefab;

	// Use this for initialization
	void Start () {
	}

    void DoThings()
    {
        
        if (Network.peerType == NetworkPeerType.Client)
        {
            PlayerData.Me.gameObject = (GameObject)Network.Instantiate(player1prefab, new Vector3(9.5f, 1, -12), Quaternion.identity, 0);
        }
        else if (Network.peerType == NetworkPeerType.Server)
        {
            PlayerData.Me.gameObject = (GameObject)Network.Instantiate(player2prefab, new Vector3(7.5f, 1, -12), Quaternion.identity, 0);
        }
        else
        {
            Debug.Log("NOT CONNECTED");
            PlayerData.Me.gameObject = (GameObject)Instantiate(player1prefab, new Vector3(9.5f, 1, -12), Quaternion.identity);
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
