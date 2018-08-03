using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Irvin Naylor
 * Last Change: 8/1/18
 * Object: GameManager
 * What script does: Controls implementation and determination of winner. Displays the right text 
 * List of Fields: 
 *          - someScript, acts as a way into the globaltimer function
 * List of Methods:
 * Notes:
 * 
 */

public class WinCondition : MonoBehaviour {

    private GlobalTimer someScript;

	// Use this for initialization
	void Start () {
        someScript = GameObject.Find("Time Value").GetComponent<GlobalTimer>(); ; //makes a copy of the GlobalTimer script
		
	}
	
	// Update is called once per frame
	void Update () {


		    if (GameObject.Find("Player").GetComponent<StatsManager>().health <= 0) //Player 1 Health = 0
        {
            someScript.playerWin.text = "Player 2 Wins!";
            Destroy(gameObject); //keeps you from getting a null reference exception
            someScript.StopGame(); //calls the stop game function in the timer 
        }
            else if (GameObject.Find("Player2").GetComponent<StatsManager>().health <= 0) //Player 2 Health = 0
        {
            someScript.playerWin.text = "Player 1 Wins!";
            Destroy(gameObject);
            someScript.StopGame();
        }


	}
}
