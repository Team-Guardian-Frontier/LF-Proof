using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;

    public BoardManager boardScript;



    //Awake is always called before any Start functions
    void Awake() {

        boardScript = GetComponent<BoardManager>();
        InitGame();
        /*if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        */
    }

    void InitGame()
    {
        Debug.Log("I made a boardmanager");
    }

	void Start() {

        
      
	}
	
	void Update() {

      
    }
}
