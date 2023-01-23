using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindGameController : MonoBehaviour
{

    public void ChangeToBlue()
    {
        GameController.Instance.gameObject.GetComponent<SwapColoursScript>().ChangeColoursToBlue();
    }

    public void ChangeToRed()
    {
       GameController.Instance.gameObject.GetComponent<SwapColoursScript>().ChangeColoursToRed();
    }

    public void ChangeToYellow()
    {
        GameController.Instance.gameObject.GetComponent<SwapColoursScript>().ChangeColoursToYellow();
    }
}
