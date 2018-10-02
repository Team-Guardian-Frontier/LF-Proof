using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHandler : MonoBehaviour {

    //tempheader: this script is bound to the player, and lets them shoot and stuff.
        //dictates firing, eating, and fruit interactions with player.

    //Calls StatsManager script
    private StatsManager stats;


    //collision food object
    private Food visitor;
    //held food object
    [SerializeField]
    private Food prisoner;
    private GameObject foodObject;

    //trigger inputs
    private float triggers;



    // Use this for initialization
    void Start () {
        //load stats
        stats = (StatsManager)this.gameObject.GetComponent(typeof(StatsManager));
		
	}
	
	// Update is called once per frame
	void Update () {

        //set triggers
        triggers = Input.GetAxis("triggers1");
        



        if (prisoner !=null)
        {
            UseFood();
            CheckShoot();
        }
	}

    //Collision --> pickup
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
         * Prevent "Stealing" eggs, while also not registering it in object.
         */

        //upon collision with food
        if (other.tag == "Food")
        {
            //register other object as visitor.
            visitor = other.GetComponent<Food>();


            //if collide with food that was shot, take damage
            if (visitor.foodState == Food.FoodState.Shot)
            {
                //call damage method in stats
                stats.takeDamage(visitor.getType());
                Debug.Log("hit with a " + visitor.getType());
                Destroy(other.gameObject);

            }
            //if not shot, then check to see if player already has ammo
            else if (prisoner == null)
            {
                //set it so that you now have a prisoner? idk if this is useful. //%Spot to optimize%, can check the state the prisoner, before taking it as a prisoner. not much damage.
                prisoner = visitor;
                     //DEBUG: tells you state of prisoner and type
                Debug.Log("prisonerstate" + prisoner.foodState);
                Debug.Log("prisonerType" + prisoner.getType());
                Debug.Log("PrisonerName" + prisoner.name);

                if (prisoner.foodState == Food.FoodState.Held)                           
                {
                    //clear prisoner details if bump into others
                    prisoner = null;
                    foodObject = null;
                }
                else
                {
                    //physically capture the object (it now follows)  %optimize% unless there's a glitch, do the prisoner = visitor; here.
                    prisoner.Pickup(this.gameObject);
                    foodObject = prisoner.gameObject;

                }

            }


        }

    }

    //method to call eat health function in stats manager, delete object, and clear prisoner.
    public void UseFood()
    {
        if (triggers > .5)
        {
            stats.eatFood(prisoner.getType());
            Destroy(foodObject);
            prisoner = null;
            foodObject = null;
        }
    }

    //method to launch the food, and clear necesary local variables.
    public void CheckShoot()
    {
        if (triggers < -.5)
        {
            GameObject player = GameObject.Find("Player");
            MousePos scriptref = (MousePos)player.GetComponent(typeof(MousePos));
            float launchAngle = scriptref.RAngle;
            prisoner.Launched(launchAngle);

            prisoner = null;
            foodObject = null;
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
