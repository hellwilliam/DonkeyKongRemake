using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour
{
	public string connectToIP = "127.0.0.1";
	public int connectPort = 25001;
    public string nickname;

    public const int MAX_PLAYERS = 4;
    public PlayerData[] players = new PlayerData[MAX_PLAYERS];

    public PlayerData FindByNetworkPlayer(NetworkPlayer player)
    {
        foreach (PlayerData playerData in players)
        {
            if (playerData.player == player)
            {
                return playerData;
            }
        }
        return null;
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        PlayerData quitter = FindByNetworkPlayer(player);
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            if (players[i] == quitter)
            {
                players[i] = null;
                break;
            }
        }
    }

    [RPC]
    public void SetName(string name, NetworkMessageInfo info)
    {
        PlayerData player = new PlayerData();
        player.name = name;
        player.player = info.sender;
        int playerIndex = -1;
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            if (players[i] == null)
            {
                playerIndex = i;
                break;
            }
        }
        if (playerIndex >= 0)
        {
            players[playerIndex] = player;
            Debug.Log("PLAYER LOGGED IN AS " + player.name);
        }
        else
        {
            Debug.Log("CANNOT LOG IN PLAYER -- ALL SLOTS TAKEN");
        }
    }

    public void UpdateIP(string ip)
    {
        connectToIP = ip;
    }

    public void UpdatePort(string port)
    {
        connectPort = int.Parse(port);
    }

    public void UpdateName(string name)
    {
        nickname = name;
    }

    public void ConnectToServer()
    {
        Network.Connect(connectToIP, connectPort);
    }

    public void StartServer()
    {
        Network.InitializeServer(MAX_PLAYERS, connectPort, false);
    }

    void OnConnectedToServer()
    {
        SetName();
    }

    private void SetName()
    {
        if (string.IsNullOrEmpty(nickname))
        {
            nickname = "Anonymous" + Random.Range(0, 9999);
        }
        PlayerData.Me.name = nickname;
        Debug.Log("Player name: " + PlayerData.Me.name);
        if (Network.isServer)
        {
            NetworkMessageInfo msg = new NetworkMessageInfo();
            SetName(nickname, msg);
        }
        else
        {
            networkView.RPC("SetName", RPCMode.Server, nickname);
        }
    }

    void OnServerInitialized()
    {
        SetName();
    }
}
