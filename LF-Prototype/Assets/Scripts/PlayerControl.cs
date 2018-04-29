using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This script is for controlling player movement on an 8-directional movement
 * It also serves as Sprite changing
 */

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    private float CDIAG = (Mathf.Sqrt(2) / 2);
    private const float togDead = .5f;

    void Start() {
        myRigidbody = (Rigidbody2D)this.gameObject.GetComponent(typeof(Rigidbody2D));
        myRigidbody.freezeRotation = true;
	}

    private float horiz;
    private float verti;

    void ControllerCheck() {
        horiz = Input.GetAxis("Horizontal");
        verti = Input.GetAxis("Vertical");


        if (horiz > togDead)
            horiz = 1;
        if (horiz < -togDead)
            horiz = -1;
        if (verti > togDead)
            verti = 1;
        if (verti < -togDead)
            verti = -1;
    }

     
	
	void Update () {

        ControllerCheck();
        Movement();
        
    }

    void Movement(){

        if (verti == -1 && horiz == 1)
        {
            //NE
            transform.Translate(Vector2.right * moveSpeed * CDIAG);
            transform.Translate(Vector2.up * moveSpeed * CDIAG);
        }
        else if (verti == 1 && horiz == -1)
        {
            //SW
            transform.Translate(Vector2.left * moveSpeed * CDIAG);
            transform.Translate(Vector2.down * moveSpeed * CDIAG);
        }
        else if (verti == 1 && horiz == 1)
        {
            //SE
            transform.Translate(Vector2.right * moveSpeed * CDIAG);
            transform.Translate(Vector2.down * moveSpeed * CDIAG);
        }
        else if (verti == -1 && horiz == -1)
        {
            //NW
            transform.Translate(Vector2.left * moveSpeed * CDIAG);
            transform.Translate(Vector2.up * moveSpeed * CDIAG);
        }
        else if (horiz == 1)
        {
            //E
            transform.Translate(Vector2.right * moveSpeed);
        }
        else if (horiz == -1)
        {
            //W
            transform.Translate(-Vector2.right * moveSpeed);
        }
        else if (verti == -1)
        {
            //N
            transform.Translate(Vector2.up * moveSpeed);
        }
        else if (verti == 1)
        {
            //S
            transform.Translate(-Vector2.up * moveSpeed);
        }

    }
}
