using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour
{
	public string connectToIP = "127.0.0.1";
	public int connectPort = 25001;
    public string nickname;

    public const int MAX_PLAYERS = 4;
    public PlayerData[] players = new PlayerData[MAX_PLAYERS];

    void Start()
    {
    }

    public PlayerData FindByNetworkPlayer(NetworkPlayer player)
    {
        foreach (PlayerData playerData in players)
        {
            if (playerData != null && playerData.player == player)
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
    public void SendName(string name, NetworkMessageInfo info)
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

    //void OnGUI()
    //{
    //    if (Network.peerType == NetworkPeerType.Disconnected)
    //    {
    //        // We are currently disconnected: Not a client or host
    //        GUILayout.Label("Connection status: Disconnected");

    //        this.connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
    //        this.connectPort = int.Parse(GUILayout.TextField(this.connectPort.ToString()));

    //        GUILayout.BeginVertical();

    //        if (GUILayout.Button("Connect as client"))
    //        {
    //            // Connect ot the "connectToIP" and "connectPort" as entered via the GUI
    //            // Ignore the NAT for now
    //            // OBSOLETE: Network.useNat = false;
    //            Network.Connect(connectToIP, connectPort);
    //        }

    //        if (GUILayout.Button("Start Server"))
    //        {
    //            // Start a server for 32 clients u sing the "connectPort" given via the GUI
    //            // Ignore the nat for now
    //            // OBSOLETE: Network.useNat = false;
    //            // OBSOLETE: Network.InitializeServer(32, connectPort);
    //            Network.InitializeServer(32, connectPort, false);
    //        }

    //        GUILayout.EndVertical();
    //    }
    //    else
    //    {
    //        // We've got a connection!

    //        if (Network.peerType == NetworkPeerType.Connecting)
    //        {
    //            GUILayout.Label("Connection status: Connecting");
    //        }
    //        else if (Network.peerType == NetworkPeerType.Client)
    //        {
    //            GUILayout.Label("Connection status: Client!");
    //            GUILayout.Label("Ping to server: " + Network.GetAveragePing(Network.connections[0]));
    //        }
    //        else if (Network.peerType == NetworkPeerType.Server)
    //        {
    //            GUILayout.Label("Connection status: Server!");
    //            GUILayout.Label("Connections: " + Network.connections.Length);
    //            if (Network.connections.Length >= 1)
    //            {
    //                GUILayout.Label("Ping to first player: " + Network.GetAveragePing(Network.connections[0]));
    //            }
    //        }

    //        if (GUILayout.Button("Disconnect"))
    //        {
    //            Network.Disconnect(200);
    //        }
    //    }
    //}


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
        Debug.Log("Player name: " + PlayerData.Me.name);
        if (Network.isServer)
        {
            NetworkMessageInfo msg = new NetworkMessageInfo();
            SendName(nickname, msg);
            PlayerData.Me = players[0];
        }
        else
        {
            PlayerData.Me.name = nickname;
            networkView.RPC("SendName", RPCMode.Server, nickname);
        }
    }

    void OnServerInitialized()
    {
        SetName();
    }
}
