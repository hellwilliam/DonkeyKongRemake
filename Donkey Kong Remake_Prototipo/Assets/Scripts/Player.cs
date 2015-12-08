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
        set
        {
            me = value;
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

	// Use this for initialization
	void Awake () {
        Barrel.moving = true;
        BarrelManager.spawning = true;
        DoThings();
	}

    void DoThings()
    {
        
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            Debug.Log("NOT CONNECTED");
            PlayerData.Me.gameObject = (GameObject)Instantiate(player1prefab, new Vector3(9.5f, 1, -12), Quaternion.identity);
        }
        else
        {
            PlayerData.Me.gameObject = (GameObject)Network.Instantiate(PlayerData.Me.prefab, new Vector3(9.5f, 1, -12), Quaternion.identity, 0);
        }
    }
}
