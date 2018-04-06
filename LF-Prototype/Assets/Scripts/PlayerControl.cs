using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for controlling player movement on an 8-directional movement
//It also serves as Sprite changing

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    private float CDIAG = (Mathf.Sqrt(2)/2);



    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();

	}
	
	
	void Update () {

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * CDIAG);
            transform.Translate(Vector2.up * moveSpeed * CDIAG);
        }
        else
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.left * moveSpeed * CDIAG);
            transform.Translate(Vector2.down * moveSpeed * CDIAG);
        }
        else
        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.right * moveSpeed * CDIAG);
            transform.Translate(Vector2.down * moveSpeed * CDIAG);
        }
        else
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.left * moveSpeed * CDIAG);
            transform.Translate(Vector2.up * moveSpeed * CDIAG);
        }
        else
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed);

        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * moveSpeed);
            
        }
        else
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed);
            
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * moveSpeed);
            
        }
        
        

    }
}
