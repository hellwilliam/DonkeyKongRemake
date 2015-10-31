using UnityEngine;
using System.Collections.Generic;

public class Chat : MonoBehaviour
{

    public bool usingChat; // can be used to determine if we need tostop player movement since were chatting

    GUISkin skin;
    bool showChat;

    private string inputField = string.Empty;
    private Vector2 scrollPosition;
    private int width = 500;
    private int height = 180;
    private string playerName;
    private float lastUnfocusTime;
    private Rect window;

    // Server-only playerlist
    private List<PlayerNode> playerList = new List<PlayerNode>();
    class PlayerNode
    {
        public string PlayerName { get; set; }
        public NetworkPlayer NetworkPlayer { get; set; }
    }

    private List<ChatEntry> chatEntries = new List<ChatEntry>();
    class ChatEntry
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }

    void Awake()
    {
        this.window = new Rect(Screen.width / 2.0f - this.width / 2.0f, Screen.height - this.height + 5, this.width, this.height);

        // We get the name from the masterserver example, if you entered your name there ;)
        this.playerName = PlayerPrefs.GetString("playerName", string.Empty);
        if (this.playerName == null || this.playerName.Trim() == string.Empty)
        {
            this.playerName = "RandomName" + Random.Range(1, 999);
        }
    }

    // Client method
    void OnConnectedToServer()
    {
        this.ShowChatWindow();
        this.networkView.RPC("TellServerOurName", RPCMode.Server, this.playerName);
        // // We could have also announcted ourselves:
        // addGameChatMessage(this.playerName + " joined the chat");
        // // But using "TellServer.." we build a list of active players which we can use for other stuff as well.
    }

    // Server method
    void OnServerInitialized()
    {
        this.ShowChatWindow();
        // no support for sending an RPC on the server to the server itself :(
        PlayerNode newEntry = new PlayerNode();
        newEntry.PlayerName = this.playerName;
        newEntry.NetworkPlayer = Network.player;
        this.playerList.Add(newEntry);
        this.AddGameChatMessage(this.playerName + " joined the chat");
    }

    // A handy wrapper to get the PlayerNode by networkPlayer
    PlayerNode GetPlayerNode(NetworkPlayer networkPlayer)
    {
        foreach (PlayerNode entry in this.playerList)
        {
            if (entry.NetworkPlayer == networkPlayer)
            {
                return entry;
            }
        }

        Debug.LogError("GetPlayerNode: Requested a playernode of non-existing player!");
        return null;
    }

    // Server function
    void OnPlayerDisconnected(NetworkPlayer player)
    {
        this.AddGameChatMessage("Player disconnected from: " + player.ipAddress + ": " + player.port);

        // Remove player from the server list
        this.playerList.Remove(this.GetPlayerNode(player));
    }

    void OnDisconnectedFromServer()
    {
        this.CloseChatWindow();
    }

    // Server function
    void OnPlayerConnected(NetworkPlayer player)
    {
        this.AddGameChatMessage("Player connected from: " + player.ipAddress + ":" + player.port);
    }

    [RPC]
    // Sent by newly connected clients, received by server
    void TellServerOurName(string name, NetworkMessageInfo info)
    {
        PlayerNode newEntry = new PlayerNode();
        newEntry.PlayerName = name;
        newEntry.NetworkPlayer = info.sender;
        this.playerList.Add(newEntry);

        this.AddGameChatMessage(name + " joined the chat");
    }

    void CloseChatWindow()
    {
        this.showChat = false;
        this.inputField = string.Empty;
        this.chatEntries = new List<ChatEntry>();
    }

    void ShowChatWindow()
    {
        this.showChat = true;
        this.inputField = string.Empty;
        this.chatEntries = new List<ChatEntry>();
    }

    void OnGUI()
    {
        if (!this.showChat)
        {
            return;
        }

        GUI.skin = skin;

        if (Event.current.type == EventType.keyDown && 
            Event.current.character == '\n' && 
            this.inputField.Length == 0)
        {
            if (this.lastUnfocusTime + 0.25f < Time.time)
            {
                this.usingChat = true;
                GUI.FocusWindow(5);
                GUI.FocusControl("Chat input field");
            }
        }

        this.window = GUI.Window(5, this.window, GlobalChatWindow, string.Empty);
    }

    void GlobalChatWindow(int id)
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUILayout.EndVertical();

        // Begin a scroll view. All rects are calculated automatically - 
        // it will use up any available screen space and make sure contents flow correctly.
        // This is kept small with the last two parameters to force scrollbars to appear.
        this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition);

        foreach (ChatEntry entry in this.chatEntries)
        {
            GUILayout.BeginHorizontal();

            if (entry.Name == string.Empty) // Game message
            {
                GUILayout.Label(entry.Text);
            }
            else
            {
                GUILayout.Label(entry.Name + ": " + entry.Text);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(3);
        }

        // End the scrollview we began above.
        GUILayout.EndScrollView();

        if (Event.current.type == EventType.keyDown && Event.current.character == '\n' && this.inputField.Length > 0)
        {
            this.HitEnter(this.inputField);
        }

        GUI.SetNextControlName("Chat input field");
        this.inputField = GUILayout.TextField(this.inputField);

        if (Input.GetKeyDown("mouse 0"))
        {
            if (this.usingChat)
            {
                this.usingChat = false;
                GUI.UnfocusWindow(); // Deselect chat
                this.lastUnfocusTime = Time.time;
            }
        }
    }

    void HitEnter(string msg)
    {
        msg = msg.Replace("\n", string.Empty);
        this.networkView.RPC("ApplyGlobalChatText", RPCMode.All, this.playerName, msg);
        this.inputField = string.Empty; // Clear line
        GUI.UnfocusWindow(); // Deselect chat
        this.lastUnfocusTime = Time.time;
        this.usingChat = false;
    }

    [RPC]
    void ApplyGlobalChatText(string name, string msg)
    {
        ChatEntry entry = new ChatEntry();
        entry.Name = name;
        entry.Text = msg;

        this.chatEntries.Add(entry);

        // Remove old entries
        if (this.chatEntries.Count > 4)
        {
            this.chatEntries.RemoveAt(0);
        }

        this.scrollPosition.y = 1000000;
    }

    // Add game messages
    void AddGameChatMessage(string str)
    {
        this.ApplyGlobalChatText(string.Empty, str);
        if (Network.connections.Length > 0)
        {
            this.networkView.RPC("ApplyGlobalChatText", RPCMode.Others, string.Empty, str);
        }
    }
}

