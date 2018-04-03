using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script is for the mouse position and getting the sprite (rectangle) to look at the cursor  
 * 
 */

public class MousePos : MonoBehaviour {

    //Sprite Stuff
    public Sprite N, NE, E, SE, S, SW, W, NW;

    public float displayangle;

    // Use this for initializing
    void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        /*
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y
        );

        transform.up = direction;
        */
        displayangle = MouseAngle();
        MouseRotation();
        
    }
    
    //Utility methods
    
    // Calculate angle from character to mouse, for purposes of rotation or aim
    float MouseAngle()
    {
        //calculate manually.
        Vector3 charPos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        float angle = Vector3.Angle(charPos, mousePos);
        return angle;
    }

    //Changes sprite according to mouse angle
    void MouseRotation()
    {
        //call mouse angle
        float angle = MouseAngle();

        //Switch case to change sprite
        if (angle <= 22.5 && angle > -22.5)
        {
            //East
            this.GetComponent<SpriteRenderer>().sprite = E;
        }
        else if (angle <= 67.5 && angle > 22.5)
        {
            //Northeast
            this.GetComponent<SpriteRenderer>().sprite = NE;
        }
        else if (angle <= 112.5 && angle > 67.5)
        {
            //North
            this.GetComponent<SpriteRenderer>().sprite = N;
        }
        else if (angle <= 157.5 && angle > 112.5)
        {
            //Northwest
            this.GetComponent<SpriteRenderer>().sprite = NW;
        }
        //negative
        else if (angle <= -22.5 && angle > -67.5)
        {
            //Southeast
            this.GetComponent<SpriteRenderer>().sprite = SE;
        }
        else if (angle <= -67.5 && angle > -112.5)
        {
            //South
            this.GetComponent<SpriteRenderer>().sprite = S;
        }
        else if (angle <= -112.5 && angle > -157.5)
        {
            //Southwest
            this.GetComponent<SpriteRenderer>().sprite = SW;
        }
        else if (angle <= -157.5 || angle >157.5)
        {
            //West
            this.GetComponent<SpriteRenderer>().sprite = W;
        }


    }

}
