using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject connectPanel;
    public GameObject networkOverlay;

	// Use this for initialization
	void Start () {
        connectPanel.SetActive(true);
        networkOverlay.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnConnectedToServer()
    {
        Debug.Log("This CLIENT has connected to a server");
        connectPanel.SetActive(false);
        networkOverlay.SetActive(true);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized and ready");
        connectPanel.SetActive(false);
        networkOverlay.SetActive(true);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("This SERVER OR CLIENT has disconnected from a server");
        connectPanel.SetActive(true);
        networkOverlay.SetActive(false);
    }
}
