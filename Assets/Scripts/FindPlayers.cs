using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class FindPlayers : MonoBehaviourPunCallbacks
{ 
    public GameObject[] ArrayOfPlayers;


    public void Awake()
    {
        StartCoroutine(FindTheUsers(false));
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player has entered the room!");
        StartCoroutine(FindTheUsers(true));
    }

    
    IEnumerator FindTheUsers(bool isMaster)
    {
        yield return new WaitForSeconds(1.0f);
        ArrayOfPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (ArrayOfPlayers.Length == 2)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                GameObject tempVar = ArrayOfPlayers[1];
                ArrayOfPlayers[1] = ArrayOfPlayers[0];
                ArrayOfPlayers[0] = tempVar;
            }
            Debug.Log("Enough players, the game can now commence");
            GameController.Instance.AddPlayers(ArrayOfPlayers[0], ArrayOfPlayers[1]);
        }
        else
        {
            GameController.Instance.AddPlayers(ArrayOfPlayers[0], null);
            Debug.Log("Not Enough Players to Begin");
        }
    }
}
