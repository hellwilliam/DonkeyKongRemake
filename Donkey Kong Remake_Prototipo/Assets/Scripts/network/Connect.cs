using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour
{
	public string connectToIP = "127.0.0.1";
	public int connectPort = 25001;

    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            // We are currently disconnected: Not a client or host
            GUILayout.Label("Connection status: Disconnected");

            this.connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
            this.connectPort = int.Parse(GUILayout.TextField(this.connectPort.ToString()));

            GUILayout.BeginVertical();

            if (GUILayout.Button("Connect as client"))
            {
                // Connect ot the "connectToIP" and "connectPort" as entered via the GUI
                // Ignore the NAT for now
                // OBSOLETE: Network.useNat = false;
                Network.Connect(connectToIP, connectPort);
            }

            if (GUILayout.Button("Start Server"))
            {
                // Start a server for 32 clients u sing the "connectPort" given via the GUI
                // Ignore the nat for now
                // OBSOLETE: Network.useNat = false;
                // OBSOLETE: Network.InitializeServer(32, connectPort);
                Network.InitializeServer(32, connectPort, false);
            }

            GUILayout.EndVertical();
        }
        else
        {
            // We've got a connection!

            if (Network.peerType == NetworkPeerType.Connecting)
            {
                GUILayout.Label("Connection status: Connecting");
            }
            else if (Network.peerType == NetworkPeerType.Client)
            {
                GUILayout.Label("Connection status: Client!");
                GUILayout.Label("Ping to server: " + Network.GetAveragePing(Network.connections[0]));
            }
            else if (Network.peerType == NetworkPeerType.Server)
            {
                GUILayout.Label("Connection status: Server!");
                GUILayout.Label("Connections: " + Network.connections.Length);
                if (Network.connections.Length >= 1)
                {
                    GUILayout.Label("Ping to first player: " + Network.GetAveragePing(Network.connections[0]));
                }
            }

            if (GUILayout.Button("Disconnect"))
            {
                Network.Disconnect(200);
            }
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

    public void ConnectToServer()
    {
        Network.Connect(connectToIP, connectPort);
    }

    public void StartServer()
    {
        Network.InitializeServer(32, connectPort, false);
    }


    //// Client functions called by Unity
    //void OnConnectedToServer()
    //{
    //    Debug.Log("This CLIENT has connected to a server");
    //    gameObject.SetActive(false);
    //}

    //void OnServerInitialized()
    //{
    //    Debug.Log("Server initialized and ready");
    //    gameObject.SetActive(false);
    //}

    //void OnDisconnectedFromServer(NetworkDisconnection info)
    //{
    //    Debug.Log("This SERVER OR CLIENT has disconnected from a server");
    //    gameObject.SetActive(true);
    //}

    //void OnFailedToConnect(NetworkConnectionError error)
    //{
    //    Debug.Log("Could not connect to server: " + error);
    //}

    //// Server functions called by Unity
    //void OnPlayerConnected(NetworkPlayer player)
    //{
    //    Debug.Log("Player connected from: " + player.ipAddress + ":" + player.port);
    //}

    //void OnPlayerDisconnected(NetworkPlayer player)
    //{
    //    Debug.Log("Player disconnected from: " + player.ipAddress + ":" + player.port);
    //}

    //// OTHERS:
    //void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    //{
    //    Debug.Log("Could not connect to master server: " + info);
    //}

    //void OnNetworkInstantiate(NetworkMessageInfo info)
    //{
    //    Debug.Log("New object instantiated by " + info.sender);
    //}

    //void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    //{
    //    // Custom code here
    //}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
