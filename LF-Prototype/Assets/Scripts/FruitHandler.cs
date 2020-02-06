using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHandler : MonoBehaviour {

    //tempheader: this script is bound to the player, and lets them shoot and stuff.
        //dictates firing, eating, and fruit interactions with player.
        /*
         * Input values:
         * Player 1 - "triggers1"
         * Player 2 - "triggers2"
         * 
         */

    //Calls StatsManager script
    private StatsManager stats;


    //collision food object
    private Food visitor;
    //held food object
    [SerializeField]
    private Food prisoner;
    private GameObject prisonerOBJ;

    //trigger inputs
    private float triggers;

    //INPUT STRINGS
    public string triggersInput;


    // Use this for initialization
    void Start () {
        //load stats
        stats = (StatsManager)this.gameObject.GetComponent(typeof(StatsManager));
		
	}
	
	// Update is called once per frame
	void Update () {

        //set triggers
        triggers = Input.GetAxis(triggersInput);
        



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
                visitor.Smash();

                //HitSound
                FindObjectOfType<AudioManager>().Play("HitSound");

            }
            //if not shot, then check to see if player already has ammo
            else if (prisoner == null)
            {
                    

                if (visitor.foodState != Food.FoodState.Held)                      
                {

                    prisoner = visitor;
                    //physically capture the object (it now follows)  %optimize% unless there's a glitch, do the prisoner = visitor; here.
                    prisoner.Pickup(this.gameObject);
                    prisonerOBJ = prisoner.gameObject;

                    //sound
                    FindObjectOfType<AudioManager>().Play("PickUpSound");

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
            prisoner.Smash();
            BailPrisoner();


            //sound
            FindObjectOfType<AudioManager>().Play("EatSound");
        }
    }

    //method to launch the food, and clear necesary local variables.
    public void CheckShoot()
    {
        if (triggers < -.5)
        {
        
            MousePos scriptref = this.gameObject.GetComponent<MousePos>();
            float launchAngle = scriptref.RAngle;
            prisoner.Launched(launchAngle);

            BailPrisoner();

            //sound
            FindObjectOfType<AudioManager>().Play("ThrowSound");

            //get stats to increment GOthrows
            this.GetComponent<StatsManager>().CountThrow();
        }
    }

    //Only this object should ever bail itself. otherwise, it runs into issues when picking up other objects.
    private Food BailPrisoner()
    {
        Food release = prisoner;
        prisoner = null;
        prisonerOBJ = null;
        return release;
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
