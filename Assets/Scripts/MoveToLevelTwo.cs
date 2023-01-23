using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MoveToLevelTwo : MonoBehaviourPunCallbacks
{
    public int levelToMoveTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PhotonView>().RPC("MoveToLevel", RpcTarget.All, levelToMoveTo);
        }
    }
}
