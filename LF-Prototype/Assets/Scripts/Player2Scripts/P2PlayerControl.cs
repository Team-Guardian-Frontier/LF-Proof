using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * This script is for controlling player movement on an 8-directional movement
 * It also serves as Sprite changing
 */

public class P2PlayerControl : MonoBehaviour
{

    public float moveSpeed;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myBox;
    private ContactFilter2D brita;

    private float cDiag = (Mathf.Sqrt(2) / 2);
    private const float ROSEDIST = .01f;

    private const float togDead = .5f;
    private RaycastHit2D[] hitResults;

    private float horiz;
    private float verti;

    void Start()
    {
        myRigidbody = (Rigidbody2D)this.gameObject.GetComponent(typeof(Rigidbody2D));
        myRigidbody.freezeRotation = true;
        myBox = GetComponent<BoxCollider2D>();

        hitResults = new RaycastHit2D[3];

        //set up filter to just not pick up the food pickups
        brita.useTriggers = false;
    }



    void ControllerCheck()
    {
        horiz = Input.GetAxis("P2Horizontal");
        verti = Input.GetAxis("P2Vertical");


        if (horiz > togDead)
            horiz = 1;
        if (horiz < -togDead)
            horiz = -1;
        if (verti > togDead)
            verti = 1;
        if (verti < -togDead)
            verti = -1;
    }

    void Update()
    {

        ControllerCheck();
        Movement();

    }

    void Movement()
    {

        //if one button is not zero, and the other is then set speed to movespeed*cdiag.
        //if the absolute value of both buttons is 2, then set speed
        float dPos = moveSpeed;
        if (Mathf.Abs(horiz) + Mathf.Abs(verti) == 2)
            dPos = moveSpeed * cDiag;


        //horizontal movement
        if (horiz == 1)
        {
            //E
            if (CheckDirect(Vector2.right, ROSEDIST))
                transform.Translate(Vector2.right * dPos);

        }
        else if (horiz == -1)
        {
            //W
            if (CheckDirect(-Vector2.right, ROSEDIST))
                transform.Translate(-Vector2.right * dPos);

        }

        //vertical movement
        if (verti == -1)
        {
            //N
            if (CheckDirect(Vector2.up, ROSEDIST))
                transform.Translate(Vector2.up * dPos);
        }
        else if (verti == 1)
        {
            //S
            if (CheckDirect(Vector2.down, ROSEDIST))
                transform.Translate(-Vector2.up * dPos);
        }

    }

    bool CheckDirect(Vector2 finnaGo, float disty)
    {
        //Checks to see things are empty, before moving.
        bool empty = false;
        if (myBox.Cast(finnaGo, brita, hitResults, disty, false) == 0)
            empty = true;

        return empty;

    }

    //see player 1 controller for notes

}
