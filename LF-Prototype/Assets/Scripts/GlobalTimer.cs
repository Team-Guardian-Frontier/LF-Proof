using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * Author: Irvin Naylor
 * Last Change: 8/1/18
 * Object: Timer
 * What script does: Controls implementation of the global timer
 * List of Fields: 
 *      - startingTime: float value containing time of round left
 *      - theText: simply gets the text feild from the UI
 *      - timeUpText: Text containing "Time's Up Message"
 *      - playerWin: Text determining which player won the game 
 * List of Methods:
 * Notes:
 * 
 */

public class GlobalTimer : MonoBehaviour {
    public float startingTime;
    private Text theText;
    public Text timeUpText;
    public Text playerWin;

	// Use this for initialization
	void Start () {

        //getting the text
        theText = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

        //subtracts time from deltatime
        startingTime -= Time.deltaTime;

        //updates time onscreen
        theText.text = "" + Mathf.Ceil(startingTime);

        //when time reaches 0
        if (startingTime <= 0)
        {
            /*
            * Will be replaced with the code to restart the timer
            */

            startingTime = 10;
            //resets the time immediately (could potentially hold a delay)
            //This is where the hunger damage takes place
        



            //timeUpText.text = "Time's up!!";
            //Destroy(gameObject); //display time's up, first of all

            //if (GameObject.Find("Player").GetComponent<StatsManager>().health > GameObject.Find("Player2").GetComponent<StatsManager>().health)
            //{
            //    playerWin.text = "Player 1 Wins"; //if player 1's health > player 2's health, this message is displayed
            //}
            //else if (GameObject.Find("Player").GetComponent<StatsManager>().health < GameObject.Find("Player2").GetComponent<StatsManager>().health)
            //{
            //    playerWin.text = "Player 2 Wins"; //vice versa
            //}

         //Notes: we can use this as a base to determine who has higher what
         // GameObject.Find("Object") finds an object
         //GetComponent<ScriptName>().variable pulls a variable from a script instance.
            


        }

        
        
	}

    //Stops the game completely
    //(Will work as a pause feature for now, will work on a proper one sooon)
    public void StopGame()
    {
        Time.timeScale = 0;
        //disable movement here
    }
    

}
