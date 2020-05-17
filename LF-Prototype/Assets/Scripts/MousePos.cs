using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is for aiming, and changing the animator to match.
 * 
 * Input Values:
 * Player 1 - "AimX", "AimY"
 * Player 2 - "P2AimX", "P2AimY"
 */

public class MousePos : MonoBehaviour {
    // Sprite variables
    public Camera mainCam;
    public Animator anim;

    [SerializeField] private int lookdir; //direction to animate. 0=S, 1=SE, 2=E, 3=NW, 4=N.
    [SerializeField] private bool flipdir; //flip val for northeast, southwest, and west.
    private Vector3 TempScale;
    

    //input strings
    public string aimXInput;
    public string aimYInput;
    //Angle Caluclations
    private float aimX;
    private float aimY;
    public float RAngle; //the final angle

    //Toggle Deadzone
    public float togDead;


    void Start()
    {
        TempScale = transform.localScale;
    }

    void Update()
    {
        //Getting input from axis
        aimX = Input.GetAxis(aimXInput);
        aimY = -(Input.GetAxis(aimYInput));

        //find difference dead zone (circle)
        float aimDelta = Mathf.Sqrt(Mathf.Pow(aimX, 2) + Mathf.Pow(aimY, 2));


        

        if (aimDelta > togDead) //if diff is greater than deadzone
        {

            //set direction
            if (aimX > togDead)
                aimX = 1;
            if (aimX < -togDead)
                aimX = -1;
            if (aimY > togDead)
                aimY = 1;
            if (aimY < -togDead)
                aimY = -1;

            StickAngle(); //calculate angle
            MouseRotation(); //set sprite

        }

        

    }
    
    // Calculate angle from controller
    void StickAngle() {

        //2 components physics
        RAngle = (Mathf.Atan2(aimY, aimX) * Mathf.Rad2Deg);

    }

    // Changes sprite according to mouse angle
    void MouseRotation() {
        // call mouse angle
        float angle = RAngle;

        //current state


        // Switch case to change sprite
        if (angle <= 22.5 && angle > -22.5) {  // East
            lookdir = 2;
            flipdir = false;
        } else if (angle <= 67.5 && angle > 22.5) { // Northeast
            lookdir = 3;
            flipdir = true;
        } else if (angle <= 112.5 && angle > 67.5) { // North
            lookdir = 4;
            flipdir = false;
        } else if (angle <= 157.5 && angle > 112.5) { // Northwest
            lookdir = 3;
            flipdir = false;
        }
        else if (angle <= -22.5 && angle > -67.5) { // Southeast
            lookdir = 1;
            flipdir = false;
        } else if (angle <= -67.5 && angle > -112.5) { // South
            lookdir = 0;
            flipdir = false;
        } else if (angle <= -112.5 && angle > -157.5) { // Southwest
            lookdir = 1;
            flipdir = true;
        } else if (angle <= -157.5 || angle > 157.5) { // Weast
            lookdir = 2;
            flipdir = true;
        }

        //Set Animator and Sprite
        anim.SetInteger("lookdir", lookdir);
        anim.SetBool("flipdir", flipdir);

        //debug
        Debug.Log("Angle: " + angle + ", lookdir: " + lookdir + ", flipdir:" + flipdir);
    }
}
