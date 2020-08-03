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

public class PauseFunction : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject PauseMenuUI;

    //public static bool lets any of the other objects access the menu. if this object is destroyed, should be fine.
    //just start as false whenever this object is instantiated, bc then game is running.
    private void Start()
    {
        isPaused = false;
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

            //Resume minus sound
            PauseMenuUI.SetActive(false);
            //Time.timeScale = 1;  already set.
            isPaused = false;
        }

        //toggle with the boolean. this way, other settings can "pause" the game, without pulling up the UI.

        if (isPaused == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    //Activate Pause
    void Pause()
    {
        //Make PauseMenu Active
        PauseMenuUI.SetActive(true);

        //Pause Sound
        FindObjectOfType<AudioManager>().PauseAudio();


        isPaused = true;
        //timescale already set in update.

    }

    void Resume()
    {
        //Deactivate PauseMenu
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1;

        //Resume Sound
        FindObjectOfType<AudioManager>().UnPauseAudio();

        isPaused = false;
        //timescale already set in update.

    }

    private void OnDestroy()
    {
        isPaused = false;
        //just in case, when this object is destroyed.
    }
}
