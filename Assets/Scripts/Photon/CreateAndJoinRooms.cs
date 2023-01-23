using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI createInput;
    public TextMeshProUGUI joinInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }    
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("First Level");
    }

    public void BackToTitleScreen()
    {
        PhotonNetwork.LeaveLobby();
    }

    public override void OnLeftLobby()
    {
        SceneManager.LoadScene("Title Screen");
    }
}
