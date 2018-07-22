using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is for the mouse position and getting the sprite (rectangle) to look at the cursor
 */

public class P2MousePos : MonoBehaviour
{
    // Sprite Stuff
    public Sprite N, NE, E, SE, S, SW, W, NW;
    public Camera mainCam;
    public float RAngle2;

    // Use this for initializing
    void Start()
    {

    }


    private float aimX;
    private float aimY;

    //Player 2 Feilds:

    //part of Toggle
    private float togDead = .5f;



    // Update is called once per frame
    void Update()
    {
        // Camera Rig Movement Control

        aimX = Input.GetAxis("P2AimX");
        aimY = Input.GetAxis("P2AimY");
        //tpogglele, Saves last direction. Odd issue registering.
        float aimDelta = Mathf.Sqrt(Mathf.Pow(aimX, 2) + Mathf.Pow(aimY, 2));

        if (aimDelta > togDead)
        {

            StickAngle();
            MouseRotation();

        }
    }
        //Utility methods

        // Calculate angle from character to mouse, for purposes of rotation or aim
         void StickAngle()
    {

        //calculate angle from controller
        RAngle2 = -(Mathf.Atan2(aimY, aimX) * Mathf.Rad2Deg);

    }

    // Changes sprite according to mouse angle
    void MouseRotation()
    {
        // call mouse angle
        float angle = RAngle2;

        // Switch case to change sprite
        if (angle <= 22.5 && angle > -22.5)
        {
            // west
            this.GetComponent<SpriteRenderer>().sprite = E;
        }
        else if (angle <= 67.5 && angle > 22.5)
        {
            // Northwest
            this.GetComponent<SpriteRenderer>().sprite = NE;
        }
        else if (angle <= 112.5 && angle > 67.5)
        {
            // North
            this.GetComponent<SpriteRenderer>().sprite = N;
        }
        else if (angle <= 157.5 && angle > 112.5)
        {
            // Northeast
            this.GetComponent<SpriteRenderer>().sprite = NW;
        }
        // negative
        else if (angle <= -22.5 && angle > -67.5)
        {
            // Southwest
            this.GetComponent<SpriteRenderer>().sprite = SE;
        }
        else if (angle <= -67.5 && angle > -112.5)
        {
            // South
            this.GetComponent<SpriteRenderer>().sprite = S;
        }
        else if (angle <= -112.5 && angle > -157.5)
        {
            // Southeast
            this.GetComponent<SpriteRenderer>().sprite = SW;
        }
        else if (angle <= -157.5 || angle > 157.5)
        {
            // East
            this.GetComponent<SpriteRenderer>().sprite = W;
        }
    }
}

