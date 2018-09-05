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
    
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.Escape)) //press escape at any time
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //scene resets
        }
	}
}
