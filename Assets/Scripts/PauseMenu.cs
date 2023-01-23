using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public GameObject UIMenu;
    private bool gameState = true;

    private void Awake()
    {
        GameObject tempUIMenu = GameObject.FindGameObjectWithTag("Pause Menu");
        if (tempUIMenu == null)
        {
            Debug.Log("UIMenu cannot be found");
        }
        else
        {
            UIMenu = tempUIMenu.GetComponentInChildren<Canvas>(true).gameObject;
            GetComponent<RPCMethods>().UIMenu = UIMenu;
            UIMenu.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (UIMenu == null)
            {
                Debug.Log("UIMenu cannot be found");
                UIMenu = GameObject.FindGameObjectWithTag("Pause Menu");
            }
            ChangeState();
        }
    }
    private void ChangeState()
    {
        gameState = !gameState;
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
}
