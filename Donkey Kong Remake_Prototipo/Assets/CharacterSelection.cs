﻿using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPlayerConnected(NetworkPlayer player)
    {
        if (Network.isServer)
        {
            
        }
    }
}