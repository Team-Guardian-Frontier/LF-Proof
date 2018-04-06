using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for controlling player movement on an 8-directional movement
//It also serves as Sprite changing

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;



    //Sprite Stuff
    



    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
		


    }
}
