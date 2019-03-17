using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Irvin Naylor
 * Last Change: 3/17/19
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
    public GameObject EndScreen;

    //players, ASSIGN
    public GameObject Player1;
    public GameObject Player2;

    //counter variables end game
    private double GOaccuracy1;
    private double GOaccuracy2;

    private double GOcarbPercent1;
    private double GOproteinPercent1;
    private double GOveggiesPercent1;

    private double GOcarbPercent2;
    private double GOproteinPercent2;
    private double GOveggiesPercent2;


    // Use this for initialization
    void Start () {
        someScript = GameObject.Find("Time Value").GetComponent<GlobalTimer>(); ; //makes a copy of the GlobalTimer script
		
	}
	


	// Update is called once per frame
	void Update () {

        
        //if player is destroyed.

        if (Player1.GetComponent<StatsManager>().health <= 0) //Player 1 Health = 0
        {
            
            //enable end screen
            EndScreen.SetActive(true);
            
            CalculateVars();
            //add code that gets all the other variables from stats, calculates vars in this, and displays in text.

            someScript.playerWin.text = "Player 2 Wins!";
            //Destroy(gameObject); //keeps you from getting a null reference exception
            someScript.StopGame(); //calls the stop game function in the timer 

            

            Restart.isPaused = true; //set game to pause, so everything is paused. but not game menu tho.
        }
            else if (Player2.GetComponent<StatsManager>().health <= 0) //Player 2 Health = 0
        {
            //enable end screen
            

            EndScreen.SetActive(true);
            
            CalculateVars();
            //add code that gets all the other variables from stats, calculates vars in this, and displays in text.

            someScript.playerWin.text = "Player 1 Wins!";
            
            someScript.StopGame();

            

            Restart.isPaused = true;
        }

            


	}

    private void CalculateVars()
    {
        Debug.Log("I activated");

        StatsManager P1Stats = Player1.GetComponent<StatsManager>();
        StatsManager P2Stats = Player2.GetComponent<StatsManager>();

        double P1throws = P1Stats.GetGOthrows();
        double P2throws = P2Stats.GetGOthrows();

        double P1totalFood = (P1Stats.GetGOeaten() + P1Stats.GetGOhitten());
        double P2totalFood = (P2Stats.GetGOeaten() + P2Stats.GetGOhitten());

        //ADD data protection for divide by zero. create separate variables.
        if (P1throws == 0)
            GOaccuracy1 = 0;
        else
            GOaccuracy1 = P2Stats.GetGOhitten() / P1throws;

        if (P2throws == 0)
            GOaccuracy2 = 0;
        else
            GOaccuracy2 = P1Stats.GetGOhitten() / P2throws;

        if (P1totalFood == 0)
        {
            GOcarbPercent1 = 0;
            GOproteinPercent1 = 0;
            GOveggiesPercent1 = 0;
        }
        else
        {
            GOcarbPercent1 = P1Stats.GetGOcarbs() / P1totalFood;
            GOproteinPercent1 = P1Stats.GetGOprotein() / P1totalFood;
            GOveggiesPercent1 = P1Stats.GetGOveggies() / P1totalFood;
        }

        if ( P2totalFood == 0)
        {
            GOcarbPercent2 = 0;
            GOproteinPercent2 = 0;
            GOveggiesPercent2 = 0;
        }
        else
        { 
            GOcarbPercent2 = P2Stats.GetGOcarbs() / P2totalFood;
            GOproteinPercent2 = P2Stats.GetGOprotein() / P2totalFood;
            GOveggiesPercent2 = P2Stats.GetGOveggies() / P2totalFood;
        }

        //debug

        Debug.Log("Checkers: " + GOaccuracy1 + "2:" + GOaccuracy2 + " carbs1 " + GOcarbPercent1 + " carbs2 " + GOcarbPercent2);
        //check numbers later.
    }

}
