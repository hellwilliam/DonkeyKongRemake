using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

    public GameObject gameOverScreen;
    public CharacterMovement player;

	// Use this for initialization
	void Start () {
        gameOverScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (player.dead)
        {
            gameOverScreen.SetActive(true);
            Barrel.moving = false;
            BarrelManager.spawning = false;

            if (Input.GetKeyDown(KeyCode.R) || Input.GetButtonDown("Restart"))
            {
                Restart();
            }
        }
	}

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
        Barrel.moving = true;
        BarrelManager.spawning = true;
    }
}
