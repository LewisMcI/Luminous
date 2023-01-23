using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCMethods : MonoBehaviourPunCallbacks
{

    public GameObject UIMenu;
    private GameObject[] players;
    [PunRPC]
    void MoveToLevel(int level)
    {
        if (level == 2)
        {
            PhotonNetwork.LoadLevel("Second Level");
        }
        if (level == 3)
        {
            PhotonNetwork.LoadLevel("Third Level");
        }
        if (level == 4)
        {
            PhotonNetwork.LoadLevel("End Level");
        }
        else
        {
            Debug.Log("Cannot find level");
        }
    }

    [PunRPC]
    void ChangeGameState(bool gameState)
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        if (gameState == false)
        {
            UIMenu.SetActive(gameState);
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIMenu.SetActive(gameState);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    [PunRPC]
    void ChangePlayerColour(bool isMaster, string cState)
    {
        players = GameController.Instance.GetPlayersArray();
        if (isMaster == PhotonNetwork.IsMasterClient)
        {
            players[1].GetComponentInChildren<Light>().color = GameController.Instance.FindColour(cState).colour;
            players[1].GetComponent<Colour>().SetColours(GameController.Instance.FindColour(cState).cState);
        }
        else
        {
            players[0].GetComponentInChildren<Light>().color = GameController.Instance.FindColour(cState).colour;
            players[0].GetComponent<Colour>().SetColours(GameController.Instance.FindColour(cState).cState);
        }
    }

    [PunRPC]
    void UpdateAnimations(bool isMaster, bool state)
    {
        try
        {
            players = GameController.Instance.GetPlayersArray();
            if (isMaster == PhotonNetwork.IsMasterClient && players.Length > 1)
            {
                players[1].GetComponent<PlayerMovement>().UpdateAnimations("isWalking", state);
            }
            else
            {
                players[0].GetComponentInChildren<Animator>().SetBool("isWalking", state);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error");
        }
    }
}

