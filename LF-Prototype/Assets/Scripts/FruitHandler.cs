using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHandler : MonoBehaviour {


    //Calls StatsManager script
    public StatsManager stats;


    //held food object
    private Food prisoner;
    private GameObject tempCast;

    //trigger inputs
    private float ltrigger;
    private float rtrigger;



    // Use this for initialization
    void Start () {
        //load stats
        stats = (StatsManager)this.gameObject.GetComponent(typeof(StatsManager));
		
	}
	
	// Update is called once per frame
	void Update () {
        ltrigger = Input.GetAxis("ltrig1");
        rtrigger = Input.GetAxis("rtrig1");



        if (prisoner !=null)
        {
            UseFood();
            CheckShoot();
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag == "Food")
        {
            

            if (prisoner == null)
            { 
                string pname = other.gameObject.name;
                tempCast = GameObject.Find(pname);
                prisoner = (Food)tempCast.GetComponent(typeof(Food));
                if(prisoner.isShot)
                {
                    stats.takeDamage();
                    // add food count for damage
                    Destroy(other.gameObject);
                    Debug.Log("Yeowser");
                }
                else
                    prisoner.Pickup(this.gameObject);

            }
            
        }

    }


    public void UseFood()
    {
        if (ltrigger > .5)
        {
            stats.eatFood(prisoner.foodType);
            Destroy(tempCast);

        }
    }

    public void CheckShoot()
    {
        if (rtrigger > .5)
        {
            GameObject player = GameObject.Find("Player");
            MousePos scriptref = (MousePos)player.GetComponent(typeof(MousePos));
            float launchAngle = scriptref.RAngle;
            prisoner.Launched(launchAngle);

            prisoner = null;
            tempCast = null;
        }
    }

    /*  Guide to food interaction:
     *  Fruit will have these states.
     *      - Held
     *          maybe decrease the fruit sprite size, and have it follow based on character's position.
     *      -shot
     *          passed in angle from player, and move at a consistent speed.
     *              set a specific variable to read if hit something like that.
     *  Player will have these actions:
     *      - Eat
     *          delete fruit, Read type, and add health and food type
     *      - Shoot
     *          put fruit in shot state, pass the shooting angle.
     *      
     *      - the collision
     *          Player will hold the object in a field. when colliding, check tag, 
     *              and place collider's object in field
     *              - Hit
     *          If hit with an object in shot, then read in damage and food type.
     *              if food type ever goes over, apply a flat damage. this will decrease healing, and add damage.
     * 
     * 5:16 am 4/29:
     * So this entire system is based off of finding game objects, then
     * referencing and casting the Script components, so we can read
     * values and run methods off of them. this seems to be pretty set then!
     * 
     */
}
