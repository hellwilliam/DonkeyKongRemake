using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkOverlay : MonoBehaviour {

    public Text pingLabel;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Network.isClient)
        {
            pingLabel.text = "Ping: " + Network.GetAveragePing(Network.connections[0]);
        }
	}

    public void Disconnect()
    {
        Network.Disconnect();
    }
}
