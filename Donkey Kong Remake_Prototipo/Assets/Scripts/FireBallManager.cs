using UnityEngine;
using System.Collections;

public class FireBallManager : MonoBehaviour
{
    public static bool spawning;
    public float spawnTime = 30f;
    public Transform[] spawnPoints;
    public GameObject fireball;

	// Use this for initialization
	void Start ()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);	
	}
	
    void Awake()
    {
        spawning = true;
    }
	// Update is called once per frame
	void Spawn ()
    {
        if (spawning)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            if (Network.isServer)
            {
                Network.Instantiate(fireball, spawnPoints[spawnPointIndex].position, Quaternion.identity, 0);
            }
            else if (Network.peerType == NetworkPeerType.Disconnected)
            {
                Instantiate(fireball, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            }
            
        }
	}
}
