using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //this object is the gameManager. All room state scripts are attatched here.
    private static GameManager instance = null;

    //Awake is always called before any Start functions
    void Awake() {
        //Prevent Duplicates
        /*
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
            */
        
        DontDestroyOnLoad(gameObject);
    }

	void Start() {

        
      
	}
	
	void Update() {

      
    }
}
