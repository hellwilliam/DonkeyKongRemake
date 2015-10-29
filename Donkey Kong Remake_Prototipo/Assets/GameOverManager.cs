using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

    Canvas gameOverScreen;
    public CharacterMovement player;

	// Use this for initialization
	void Start () {
        gameOverScreen = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (player.dead)
        {
            gameOverScreen.enabled = true;
            Barrel.moving = false;
            BarrelManager.spawning = false;
        }
	}
}
