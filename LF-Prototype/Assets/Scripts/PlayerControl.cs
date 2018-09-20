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

    private float cDiag = (Mathf.Sqrt(2) / 2);
    private const float ROSEDIST = .01f;

    private const float togDead = .5f;
    private RaycastHit2D[] hitResults;

    //previous position
    private Vector3 lastPosition;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.freezeRotation = true;
        myBox = GetComponent<BoxCollider2D>();

        hitResults = new RaycastHit2D[3];

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
        lastPosition = transform.position;
        ControllerCheck();
        Movement();
        
    }
    
    void Movement(){


        if (verti == -1 && horiz == 1)
        {
            //NE
            if (CheckDirect(Vector2.one, cDiag))
            {
                transform.Translate(Vector2.right * moveSpeed * cDiag);
                transform.Translate(Vector2.up * moveSpeed * cDiag);
            }
        }
        else if (verti == 1 && horiz == -1)
        {
            //SW
            if (CheckDirect(new Vector2(-1,-1), cDiag))
            {
                transform.Translate(Vector2.left * moveSpeed * cDiag);
                transform.Translate(Vector2.down * moveSpeed * cDiag);
            }
        }
        else if (verti == 1 && horiz == 1)
        {
            //SE
            //SW
            if (CheckDirect(new Vector2(1, -1),cDiag))
            {
                transform.Translate(Vector2.right * moveSpeed * cDiag);
                transform.Translate(Vector2.down * moveSpeed * cDiag);
            }
        }
        else if (verti == -1 && horiz == -1)
        {
            //NW
            //SW
            if (CheckDirect(new Vector2(-1, 1),ROSEDIST*cDiag))
            {
                transform.Translate(Vector2.left * moveSpeed * cDiag);
                transform.Translate(Vector2.up * moveSpeed * cDiag);
            }
        }
        else if (horiz == 1)
        {
            //E
            if (CheckDirect(Vector2.right,ROSEDIST))
                transform.Translate(Vector2.right * moveSpeed);

        }
        else if (horiz == -1)
        {
            //W
            if (CheckDirect(-Vector2.right,ROSEDIST))
                transform.Translate(-Vector2.right * moveSpeed);
            
        }
        else if (verti == -1)
        {
            //N
            if (CheckDirect(Vector2.up,ROSEDIST))
                transform.Translate(Vector2.up * moveSpeed);
        }
        else if (verti == 1)
        {
            //S
            if (CheckDirect(Vector2.down,ROSEDIST))
                transform.Translate(-Vector2.up * moveSpeed);
        }
        
        
    }

    bool CheckDirect(Vector2 finnaGo, float disty)
    {
        //Checks to see things are empty b4 moving.
        bool empty = false;
        if (myBox.Cast(finnaGo, hitResults, disty, true) == 0)
            empty = true;
        
        return empty;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = lastPosition;
        Debug.Log("I'm doin it" + myBox.bounciness);
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
     * Use layer masks so that it doesn't detect the pickup colliders.
     * 
     */
}
