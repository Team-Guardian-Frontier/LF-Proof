using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script is for the mouse position and getting the sprite (rectangle) to look at the cursor  
 * 
 */

public class MousePos : MonoBehaviour {

    //Sprite Stuff
    public Sprite N, NE, E, SE, S, SW, W, NW;
    public Camera mainCam;
    public float mAngle;



    
    // Use this for initializing
    void Start () {

	}

    // Update is called once per frame
    void Update() {
        
        MouseAngle();
        MouseRotation();
        
    }
    
    //Utility methods
    
    // Calculate angle from character to mouse, for purposes of rotation or aim
    void MouseAngle()
    {
        //calculate manually.
        //Mouse is in pixel Loc(ScreenPoint) and char is on World Point.
        Vector3 charPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;
        float xdist = charPos.x - mousePos.x;
        float ydist = charPos.y - mousePos.y;

        mAngle = -Mathf.Atan2(ydist, xdist) * Mathf.Rad2Deg;



    }

    //Changes sprite according to mouse angle
    void MouseRotation()
    {
        //call mouse angle
        float angle = mAngle;

        //Switch case to change sprite
        if (angle <= 22.5 && angle > -22.5)
        {
            //west
            this.GetComponent<SpriteRenderer>().sprite = W;
        }
        else if (angle <= 67.5 && angle > 22.5)
        {
            //Northwest
            this.GetComponent<SpriteRenderer>().sprite = NW;
        }
        else if (angle <= 112.5 && angle > 67.5)
        {
            //North
            this.GetComponent<SpriteRenderer>().sprite = N;
        }
        else if (angle <= 157.5 && angle > 112.5)
        {
            //Northeast
            this.GetComponent<SpriteRenderer>().sprite = NE;
        }
        //negative
        else if (angle <= -22.5 && angle > -67.5)
        {
            //Southwest
            this.GetComponent<SpriteRenderer>().sprite = SW;
        }
        else if (angle <= -67.5 && angle > -112.5)
        {
            //South
            this.GetComponent<SpriteRenderer>().sprite = S;
        }
        else if (angle <= -112.5 && angle > -157.5)
        {
            //Southeast
            this.GetComponent<SpriteRenderer>().sprite = SE;
        }
        else if (angle <= -157.5 || angle >157.5)
        {
            //East
            this.GetComponent<SpriteRenderer>().sprite = E;
        }


    }

}
