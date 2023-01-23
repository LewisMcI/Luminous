using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SwapColoursScript : MonoBehaviour
{

    private Colour colourToChangeTo;
    private GameObject[] players;
    public void ChangeColoursToRed()
    {
        colourToChangeTo = GameController.Instance.FindColour("Red");
        players = GameController.Instance.GetPlayersArray();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetPhotonView().IsMine)
            {
                Colour playerLightColour = players[i].GetComponent<Colour>();
                playerLightColour.SetNewColour(colourToChangeTo.cState, colourToChangeTo.colour);
            }
        }
    }
    public void ChangeColoursToBlue()
    {
        colourToChangeTo = GameController.Instance.FindColour("Blue");
        players = GameController.Instance.GetPlayersArray();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetPhotonView().IsMine)
            {
                Colour playerLightColour = players[i].GetComponent<Colour>();
                playerLightColour.SetNewColour(colourToChangeTo.cState, colourToChangeTo.colour);
            }
        }
    }
    public void ChangeColoursToYellow()
    {
        colourToChangeTo = GameController.Instance.FindColour("Yellow");
        players = GameController.Instance.GetPlayersArray();
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetPhotonView().IsMine)
            {
                Colour playerLightColour = players[i].GetComponent<Colour>();
                playerLightColour.SetNewColour(colourToChangeTo.cState, colourToChangeTo.colour);
            }
        }
    }
}
