using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }

    public void QuitGame()
    {
        Debug.Log("User has Quit");
        Application.Quit();
    }
}
