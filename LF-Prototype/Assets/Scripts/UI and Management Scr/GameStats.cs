using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

public class GameStats : MonoBehaviour {

    [SerializeField]private SceneInjector SceneLoad = null;
    private GlobalTimer globetime;

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

     StatsManager P1Stats;
     StatsManager P2Stats;


    private void Awake()
    {
        SceneLoad.SceneJect += inject;
    }

    #region Sceneloader events

    public void inject(InjectionDict ID)
    {
        globetime = ID.Inject<GlobalTimer>();
    }
    #endregion


    void Start ()
    {
        P1Stats = Player1.GetComponent<StatsManager>();
        P2Stats = Player2.GetComponent<StatsManager>();
    }
	


	// Update is called once per frame
	void Update () {
        
        //if player is destroyed.

        if (Player1.GetComponent<StatsManager>().health <= 0) //Player 1 Health = 0
        {
            
            
            
            CalculateVars();
            //add code that gets all the other variables from stats, calculates vars in this, and displays in text.
            SetVars();

            //enable end screen
            EndScreen.SetActive(true);

            globetime.playerWin.text = "Player 2 Wins!";
            //Destroy(gameObject); //keeps you from getting a null reference exception
            globetime.StopGame(); //calls the stop game function in the timer 

            

            PauseFunction.isPaused = true; //set game to pause, so everything is paused. but not game menu tho.
        }
            else if (Player2.GetComponent<StatsManager>().health <= 0) //Player 2 Health = 0
        {
         
            
            CalculateVars();
            //add code that gets all the other variables from stats, calculates vars in this, and displays in text.
            SetVars();

            //enable end screen
            EndScreen.SetActive(true);

            globetime.playerWin.text = "Player 1 Wins!";

            

            globetime.StopGame();

            

            PauseFunction.isPaused = true;

        }
	}

    private void CalculateVars()
    {

       

        //local calculation stats.
        double P1throws = P1Stats.GetGOthrows();
        double P2throws = P2Stats.GetGOthrows();

        double P1totalFood = (P1Stats.GetGOeaten() + P1Stats.GetGOhitten());
        double P2totalFood = (P2Stats.GetGOeaten() + P2Stats.GetGOhitten());

        //ADD data protection for divide by zero. create separate variables.
        if (P1throws == 0)
            GOaccuracy1 = 0;
        else
            GOaccuracy1 = (P2Stats.GetGOhitten() / P1throws) * 100;

        if (P2throws == 0)
            GOaccuracy2 = 0;
        else
            GOaccuracy2 = (P1Stats.GetGOhitten() / P2throws) *100;

        if (P1totalFood == 0)
        {
            GOcarbPercent1 = 0;
            GOproteinPercent1 = 0;
            GOveggiesPercent1 = 0;
        }
        else
        {
            GOcarbPercent1 = (P1Stats.GetGOcarbs() / P1totalFood) * 100;
            GOproteinPercent1 = (P1Stats.GetGOprotein() / P1totalFood) * 100;
            GOveggiesPercent1 = (P1Stats.GetGOveggies() / P1totalFood) * 100;
        }

        if ( P2totalFood == 0)
        {
            GOcarbPercent2 = 0;
            GOproteinPercent2 = 0;
            GOveggiesPercent2 = 0;
        }
        else
        { 
            GOcarbPercent2 = (P2Stats.GetGOcarbs() / P2totalFood) * 100;
            GOproteinPercent2 = (P2Stats.GetGOprotein() / P2totalFood) * 100;
            GOveggiesPercent2 = (P2Stats.GetGOveggies() / P2totalFood) * 100;
        }

        //check numbers later.
    }

    private void SetVars()
    {
        Text p1Text = EndScreen.transform.GetChild(0).gameObject.GetComponent<Text>();
        Text p2Text = EndScreen.transform.GetChild(1).gameObject.GetComponent<Text>();

        
        p1Text.text = "Player 1: \n" +
            "\n" +
            "Food Eaten: " + P1Stats.GetGOeaten() + " \n" +
            "Food Thrown: " + P1Stats.GetGOthrows() + "\n" +
            "Hits Taken: " + P1Stats.GetGOhitten() + "\n" +
            "Accuracy: " + GOaccuracy1 + "%\n" + 
            "\n" +
            "Food Groups: \n" +
            "Carbs: " + GOcarbPercent1 + "%\n" +
            "Veggies: " + GOveggiesPercent1 + "%\n" +
            "Protein: " + GOproteinPercent1 + "%";

        p2Text.text = "Player 2: \n" +
           "\n" +
           "Food Eaten: " + P2Stats.GetGOeaten() + " \n" +
           "Food Thrown: " + P2Stats.GetGOthrows() + "\n" +
           "Hits Taken: " + P2Stats.GetGOhitten() + "\n" +
           "Accuracy: " + GOaccuracy2 + "%\n" +
           "\n" +
           "Food Groups: \n" +
           "Carbs: " + GOcarbPercent2 + "%\n" +
           "Veggies: " + GOveggiesPercent2 + "%\n" +
           "Protein: " + GOproteinPercent2 + "%";


    }

}
