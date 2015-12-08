using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {
    public Connect server;
    public Chat2 chat;
    public Button startButton;
    public GameObject[] prefabs = new GameObject[Connect.MAX_PLAYERS];

    void Start()
    {
    }

    void Update()
    {
        startButton.gameObject.SetActive(Network.isServer);
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartGame();
        }
    }

	public void Select(int slot)
    {
        PlayerData.Me.prefab = prefabs[slot];
    }

    void OnConnectedToServer()
    {
        Select(0);
    }

    void OnServerInitialized()
    {
        Select(0);
    }

    void CheckIfAllReady()
    {
        bool allReady = true;
        foreach (PlayerData player in server.players)
        {
            if (player != null)
            {
                allReady = allReady && player.ready;
            }
        }
        startButton.interactable = allReady;
    }

    [RPC]
    public void SendReady(bool ready, NetworkMessageInfo info)
    {
        if (Network.isServer)
        {
            PlayerData player = server.FindByNetworkPlayer(info.sender);
            if (player != null)
            {
                player.ready = ready;
            }
            CheckIfAllReady();
        }
    }

    public void Ready(bool ready)
    {
        PlayerData.Me.ready = ready;
        PlayerData.Me.prefab = PlayerData.Me.prefab ?? prefabs[0];
        if (Network.isClient)
        {
            networkView.RPC("SendReady", RPCMode.Server, ready);
        }
        else
        {
            CheckIfAllReady();
        }
    }

    [RPC]
    public void StartGame()
    {
        if (Network.isServer)
        {
            networkView.RPC("StartGame", RPCMode.Others);
            Application.LoadLevel("Donkey Kong_Prototipo");
        }
        else
        {
            Application.LoadLevel("Donkey Kong_Prototipo");
        }
    }
}
