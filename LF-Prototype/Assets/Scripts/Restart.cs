using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: Irvin Naylor
 * Last Change: 10/15/18 - Dillon
 * What script does: Contains script for activating Pause Menu, and Restarting Game.
 * Notes: - Attached to Event System. (Good for management within scene)
 *        - Pause all sounds except pause menu?
 * 
 */

public class Restart : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject PauseMenuUI;



    private void Start()
    {
       
    }

    void Update () {


        if (Input.GetKeyDown(KeyCode.Escape)) //press escape at any time
        {


            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            //Reset the Game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            isPaused = false;
        }
    }

    //Activate Pause
    void Pause()
    {
        //Make PauseMenu Active
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0;

        //Pause Sound
        FindObjectOfType<AudioManager>().PauseAudio();


        isPaused = true;

    }

    void Resume()
    {
        //Deactivate PauseMenu
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        //Resume Sound
        FindObjectOfType<AudioManager>().UnPauseAudio();

        isPaused = false;

    }
}
