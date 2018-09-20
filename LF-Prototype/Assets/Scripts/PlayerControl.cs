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
    private BoxCollider2D myBox;
    private ContactFilter2D brita;

    private float cDiag = (Mathf.Sqrt(2) / 2);
    private const float ROSEDIST = .01f;

    private const float togDead = .5f;
    private RaycastHit2D[] hitResults;

    private float horiz;
    private float verti;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.freezeRotation = true;
        myBox = GetComponent<BoxCollider2D>();

        hitResults = new RaycastHit2D[3];

        //set up filter to just not pick up the food pickups
        brita.useTriggers = false;


	}



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

        //if one button is not zero, and the other is then set speed to movespeed*cdiag.
        //if the absolute value of both buttons is 2, then set speed
        float dPos = moveSpeed;
        if (Mathf.Abs(horiz) + Mathf.Abs(verti) == 2)
            dPos = moveSpeed * cDiag;
        

        //horizontal movement
        if (horiz == 1)
        {
            //E
            if (CheckDirect(Vector2.right,ROSEDIST))
                transform.Translate(Vector2.right * dPos);

        }
        else if (horiz == -1)
        {
            //W
            if (CheckDirect(-Vector2.right,ROSEDIST))
                transform.Translate(-Vector2.right * dPos);
            
        }

        //vertical movement
        if (verti == -1)
        {
            //N
            if (CheckDirect(Vector2.up,ROSEDIST))
                transform.Translate(Vector2.up * dPos);
        }
        else if (verti == 1)
        {
            //S
            if (CheckDirect(Vector2.down,ROSEDIST))
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



    /* Movement Methods, Pros and Cons.
     * 
     * myRigidbody.AddForce(Vector2.right*moveSpeed, ForceMode2D.Impulse);
     *      This method is useful for physics,  but it is very difficult to stop. myrigidbody.velocity is the same.
     *      
     * Character controller
     *      Seems the simplest method to move, is full of preexisting shortcuts. Problem being i have no idea how it works, can't coexist with a collider
     *      and well, is prebaked, so if you want something else, probably not gonna get it.
     *
     * private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horiz, -verti);
        GetComponent<Rigidbody2D>().velocity = targetVelocity * moveSpeed;
    }
            Most promising one. Biggest problem is that it just applies forces to the other player, that I can't deal.
     * 
     * So we're going with something I can do, and honestly is the best right now.
     * keeping the transforms, and using either a sweeptest,
     *      nvm sweeptest is 3d only
     * Or resetting the position before it can even change, either in fixedupdate or b4 it can move.
     * 
     * 
     * last two parts. fix the bumpiness on the diags.
     *      Switching to just 4 directions, so it only has two directions, with a yes or no. to limit speed, just do it buddy.
     * Use layer masks so that it doesn't detect the pickup colliders.
     * 
     * Points of optimization: get rid of unnecessary vars, sort em out, and etc.
     * use getaxisraw, and make it a single script, instead of two players. (use public vars, and get an input managing script. attatch values via that.)
     *      for controller, if you aren't doing getaxisraw, work with the ==2 for the absolute vals.
     *      
     *  Cool Note!: Weirdly enough, the space here still lets the player bump into the other player, for a bit of an adorable and cool bump.
     */
}
