using UnityEngine;
using System.Collections;

public class BarrelManager : MonoBehaviour 
{
    public static bool spawning;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public GameObject barrel;

    private static int id = 0;
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
                GameObject obj = (GameObject)Instantiate(barrel, spawnPoints[spawnPointIndex].position, Quaternion.identity);
                obj.name = obj.name + id;
                id += 1;
            }
        }
    }
}
