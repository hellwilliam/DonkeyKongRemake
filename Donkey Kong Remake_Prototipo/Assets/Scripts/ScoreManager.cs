using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    Text scorePanel;
	// Use this for initialization
	void Start () {
        score = 0;
        scorePanel = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scorePanel.text = "SCORE: " + score.ToString("D4");
	}
}
