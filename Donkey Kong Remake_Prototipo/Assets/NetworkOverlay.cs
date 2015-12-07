using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkOverlay : MonoBehaviour {

    public Text pingLabel;
    public Text nameLabel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Network.isClient)
        {
            pingLabel.text = "Ping: " + Network.GetAveragePing(Network.connections[0]);
        }
        else
        {
            pingLabel.text = "Ping: 0ms";
        }
        nameLabel.text = "Name: " + PlayerData.Me.name;
	}

    public void Disconnect()
    {
        Network.Disconnect();
    }
}
