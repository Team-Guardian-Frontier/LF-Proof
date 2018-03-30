using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for controlling player movement on an 8-directional movement
//It also serves as Sprite changing

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    private Vector3 moveInput;

    //Sprite Stuff
    public Sprite N, NE, E, SE, S, SW, W, NW;



    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
		if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed);
            this.GetComponent<SpriteRenderer>().sprite = E;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            this.GetComponent<SpriteRenderer>().sprite = SE;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * moveSpeed);
            this.GetComponent<SpriteRenderer>().sprite = W;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            this.GetComponent<SpriteRenderer>().sprite = SW;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed);
            this.GetComponent<SpriteRenderer>().sprite = N;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            this.GetComponent<SpriteRenderer>().sprite = NW;
        }


        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * moveSpeed);
            this.GetComponent<SpriteRenderer>().sprite = S;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            this.GetComponent<SpriteRenderer>().sprite = NE;
        }


    }
}
