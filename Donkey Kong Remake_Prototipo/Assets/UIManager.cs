using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject connectPanel;
    public GameObject networkOverlay;
    public GameObject chatPanel;
    public GameObject playerSelect;

	void Awake () {
        ChangeUI(Network.isClient || Network.isServer);
	}

    void ChangeUI(bool connected)
    {
        connectPanel.SetActive(!connected);
        networkOverlay.SetActive(connected);
        chatPanel.SetActive(connected);
        playerSelect.SetActive(connected);
    }

    void OnConnectedToServer()
    {
        Debug.Log("This CLIENT has connected to a server");
        ChangeUI(true);
    }

    void OnServerInitialized()
    {
        Debug.Log("Server initialized and ready");
        ChangeUI(true);
    }

    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        Debug.Log("This SERVER OR CLIENT has disconnected from a server");
        ChangeUI(false);
    }
}
