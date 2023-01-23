using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject UIMenu;
    List<GameObject> players = new List<GameObject>();

    public Colour[] coloursAvailable;
    public static GameController Instance { get; private set; }
    private bool gameBegun;
    private void Awake()
    {
        gameBegun = true;
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayers(GameObject player1, GameObject player2)
    {
        if (!players.Contains(player1) && player1 != null)
        {
            players.Add(player1);
        }
        if (!players.Contains(player2) && player2 != null)
        {
            players.Add(player2);
        }
        if (PhotonNetwork.IsMasterClient && player1 != null && player2 != null)
        {
            GameObject tempPlayer = players[0];
            players[0] = players[1];
            players[1] = tempPlayer;
        }
        gameBegun = true;
    }
    public void ResetPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i] = null;
        }
    }

    public List<GameObject> GetPlayersList()
    {
        return this.players;
    }
    
    public GameObject[] GetPlayersArray()
    {
        return this.players.ToArray();
    }

    public GameObject GetUIMenu()
    {
        return UIMenu;
    }

    public Colour FindColour(string colourToSwapTo)
    {
        for (int i = 0; i < coloursAvailable.Length; i++)
        {
            if (coloursAvailable[i].cState.ToString() == colourToSwapTo)
            {
                return coloursAvailable[i];
            }
        }

        Debug.Log("Could not find Colour");
        return null;
    }
}
