using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text;

public class Chat2 : MonoBehaviour {

    private Connect server;
    public Text chatContentPanel;
    public string currentMessage;
    public List<string> text;
    public int maxMessages = 30;
	// Use this for initialization
	void Awake () {
        server = GameObject.FindGameObjectWithTag("Server").GetComponent<Connect>();
	}
	
	// Update is called once per frame
	void Update () {
        StringBuilder builder = new StringBuilder();
        foreach (string line in text)
        {
            builder.AppendLine(line);
        }
        chatContentPanel.text = builder.ToString();
	}

    public void UpdateCurrentMessage(string message)
    {
        currentMessage = message;
    }

    public void SendMessage(string message = null)
    {
        if (Network.isServer)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = "<" + PlayerData.Me.name + "> " + currentMessage;
            }
            AddMessage(message);
            networkView.RPC("Message", RPCMode.Others, message);
        }
        else
        {
            if (string.IsNullOrEmpty(message))
            {
                message = currentMessage;
            }
            networkView.RPC("Message", RPCMode.Server, message);
        }
        currentMessage = "";
    }

    [RPC]
    public void Message(string message, NetworkMessageInfo info)
    {
        if (Network.isServer)
        {
            PlayerData player = server.FindByNetworkPlayer(info.sender);
            if (player != null)
            {
                message = "<" + player.name + "> " + message;
                networkView.RPC("Message", RPCMode.Others, message);
                AddMessage(message);
            }
        }
        else
        {
            AddMessage(message);
        }
    }

    private void AddMessage(string message)
    {
        text.Add(message);
        while (text.Count > maxMessages)
        {
            text.RemoveAt(0);
        }
    }
}
