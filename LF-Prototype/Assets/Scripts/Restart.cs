using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: Irvin Naylor
 * Last Change: 7/13/18
 * What script does: Contains way to restart game upon pressing ESC
 * Notes: - Temporary for now
 *        - Attached to GameManager object
 */

public class Restart : MonoBehaviour {

    private bool isPaused;

    private void Awake()
    {
        isPaused = false;

    }
    // Update is called once per frame
    void Update () {

   
		if( Input.GetKeyDown(KeyCode.Escape)) //press escape at any time
        {
            isPaused = !isPaused;
        }
        
        if (isPaused)
        {
            Time.timeScale = 0;

            Debug.Log("I pause");

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //scene resets
                isPaused = false;
            }
        }
        else
        {
            Time.timeScale = 1;
        }
        
	}
}
