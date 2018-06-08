using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AnnounceWinner : NetworkBehaviour {

    public NetworkManager nManager;
    public Text scoreboard;

    public List<GameObject> activePlayers = new List<GameObject>();

	void Start () {
        for(int i = 0; i < nManager.numPlayers; i++)
        {
            activePlayers.Add(GameObject.FindGameObjectWithTag("Player"));
        }
	}


    [Client]
    public void Announce(GameObject winner)
    {
        CmdAnnounce(winner);
    }

    [Command]
    void CmdAnnounce(GameObject cmdWinner)
    {
        RpcAnnounce(cmdWinner);
    }

    [ClientRpc]
    void RpcAnnounce(GameObject rpcWinner)
    {
        scoreboard.enabled = !enabled;
        scoreboard.text = rpcWinner.name + "Won!!!";
        DisplayScores();
    }

    void DisplayScores()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 4, Screen.height / 4,
           Screen.width - (Screen.width / 4), Screen.height - (Screen.height / 4)));

        GUILayout.BeginHorizontal();
        GUILayout.Label("Player:");
        GUILayout.Label("Kills:");
        GUILayout.Label("Deaths:");
        GUILayout.EndHorizontal();

        foreach (GameObject pl in activePlayers)
        {
            pl.GetComponent<characterController>().speed = 0f;

            GUILayout.BeginHorizontal();
            GUILayout.Label(pl.name);
            GUILayout.Label(pl.GetComponent<characterController>().kills.ToString());
            GUILayout.Label(pl.GetComponent<characterController>().deaths.ToString());
            GUILayout.EndHorizontal();
        }

        GUILayout.EndArea();
    }
}
