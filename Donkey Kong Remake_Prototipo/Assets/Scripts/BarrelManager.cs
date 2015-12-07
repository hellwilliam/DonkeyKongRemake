using UnityEngine;
using System.Collections;

public class BarrelManager : MonoBehaviour 
{
    public static bool spawning;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public GameObject barrel;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Awake()
    {
        spawning = true;
    }

    void Spawn()
    {
        if (spawning && (Network.isServer || Network.peerType == NetworkPeerType.Disconnected))
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            if (Network.isServer)
            {
                Network.Instantiate(barrel, spawnPoints[spawnPointIndex].position, Quaternion.identity, 0);
            }
            else
            {
                Instantiate(barrel, spawnPoints[spawnPointIndex].position, Quaternion.identity);
            }
        }
    }
}
