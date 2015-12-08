using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private Connect server;
    public static int score;
    public int scoreEditor;
    Text scorePanel;
	// Use this for initialization
	void Awake () {
        score = 0;
        scoreEditor = 0;
        scorePanel = GetComponent<Text>();
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<Connect>();
	}
	
	// Update is called once per frame
	void Update () {
        if (scoreEditor != 0)
        {
            score = scoreEditor;
            scoreEditor = 0;
        }
        scorePanel.text = "SCORE: " + score.ToString("D4");
	}
}
