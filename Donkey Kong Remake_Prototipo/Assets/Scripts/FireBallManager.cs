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
            Instantiate(fireball, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
	}
}
