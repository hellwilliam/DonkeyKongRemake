using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    public static GameOverManager instance;
    public GameObject gameOverScreen;
    public Connect server;
    public int dead;
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        dead += 1;
        if (Network.peerType == NetworkPeerType.Disconnected || dead >= Network.connections.Length)
        {
            Barrel.moving = false;
            BarrelManager.spawning = false;
            Invoke("Restart", 10f);
        }
    }

	// Use this for initialization
	void Awake () {
        gameOverScreen.SetActive(false);
        instance = this;
        GameObject serverObj = GameObject.FindGameObjectWithTag("Server");
        server = serverObj.GetComponent<Connect>();
        dead = 0;
	}

    public void Restart()
    {
        Application.LoadLevel("Connect");
    }
}
