using UnityEngine;
using System.Collections;

public class BarrelManager : MonoBehaviour 
{
    public static bool spawning = true;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public GameObject barrel;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        if (spawning)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(barrel, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
