using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colour : MonoBehaviour
{
    [SerializeField]
    public enum ColourState
    {
        Blue = 0,
        Red = 1,
        Yellow = 2
    }
    public ColourState cState;
    public Color colour;

    public void SetNewColour(ColourState newState, Color newColour)
    {
        this.colour = newColour;
        this.cState = newState;
        SetColoursForLight();
    }  

    public void SetColours(ColourState newwState)
    {
        this.cState = newwState;
        this.colour = GameController.Instance.FindColour(newwState.ToString()).colour;
    }
    public void SetColoursForLight()
    {
        GetComponent<PhotonView>().RPC("ChangePlayerColour", RpcTarget.All, PhotonNetwork.IsMasterClient, this.cState.ToString());
    }

    public Color GetColor()
    {
        return this.colour;
    }
}
