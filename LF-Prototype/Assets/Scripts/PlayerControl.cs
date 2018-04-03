using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for controlling player movement on an 8-directional movement
//It also serves as Sprite changing

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    private Vector3 moveInput;

    



    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
        
		if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed);
            
        }
        else
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * moveSpeed);
            
        }
        else
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed);
            
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * moveSpeed);
            
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            
        }
        

    }
}
