using UnityEngine;
using System.Collections;

public class PlayerSelection : MonoBehaviour {
    public Connect server;
    public GameObject[] prefabs = new GameObject[Connect.MAX_PLAYERS];

	public void Select(int slot)
    {
        PlayerData.Me.prefab = prefabs[slot];
    }

    [RPC]
    public void SelectPlayer(int slot, NetworkMessageInfo info)
    {
        if (Network.isServer)
        {
            PlayerData player = server.FindByNetworkPlayer(info.sender);
            player.prefab = prefabs[slot];
        }
        else
        {
            
        }
    }
}
