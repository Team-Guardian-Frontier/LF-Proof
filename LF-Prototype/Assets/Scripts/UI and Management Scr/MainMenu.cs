using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Enables the play button to move to the next scene
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Enables the quit button to exit out of the game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }


}
