using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * Author: Irvin Naylor
 * Last Change: 6/18/18
 * Object: Timer
 * What script does: Controls implementation of the global timer
 * List of Fields: 
 *      - startingTime: float value containing time of round left
 *      - theText: simply gets the text feild from the UI
 * List of Methods:
 * Notes:
 * 
 */

public class GlobalTimer : MonoBehaviour {
    public float startingTime;
    private Text theText;
    public Text timeUpText;

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
            timeUpText.text = "Time's up!!";
            Destroy(gameObject);

        }
        
	}

    public float getstartingTime()
    {
        return startingTime;
    }
}
