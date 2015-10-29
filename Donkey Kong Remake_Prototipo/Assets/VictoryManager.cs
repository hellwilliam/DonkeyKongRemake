using UnityEngine;
using System.Collections;

public class VictoryManager : MonoBehaviour {

    public GameObject victoryScreen;
    public CharacterMovement player;

	// Use this for initialization
	void Start () {
        victoryScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (player.won)
        {
            victoryScreen.SetActive(true);
        }
	}

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
