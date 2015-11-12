using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {
    public static GameOverManager instance;
    public GameObject gameOverScreen;

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Barrel.moving = false;
        BarrelManager.spawning = false;
    }

	// Use this for initialization
	void Start () {
        gameOverScreen.SetActive(false);
        instance = this;
	}

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        Barrel.moving = true;
        BarrelManager.spawning = true;
    }
}
