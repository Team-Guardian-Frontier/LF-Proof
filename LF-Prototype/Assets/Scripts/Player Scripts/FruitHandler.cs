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
         */

    //Calls StatsManager script
    private StatsManager stats;

    //animator
    public Animator anim;


    //collision food object
    public Food visitor;
    
    //held food object
    [SerializeField]
    private Food prisoner;

    private MousePos scriptref = null;

    //trigger inputs
    private float triggers;

    //INPUT STRINGS
    public string triggersInput;


    #region Cooldowns
    private float ThrowCool = .25f;
    private float EatCool = .25f;
    #endregion


    // Use this for initialization
    void Start () {
        //load stats
        stats = (StatsManager)this.gameObject.GetComponent(typeof(StatsManager));
        //launches food
        scriptref = this.gameObject.GetComponent<MousePos>();
    }
	
	// Update is called once per frame
	void Update () {

        //set triggers
        triggers = Input.GetAxis(triggersInput);

        #region State Machine

        //checks if food is currently held
        if (prisoner !=null)
        {
            UseFood(); //calls use food method (below)
            CheckShoot(); //calls shoot food method (below)
        }
        // PICKUP
        else if (visitor != null && visitor.player == null && visitor.foodState == Food.FoodState.None)
        {
                //physically capture the object
                prisoner = visitor;
                prisoner.Pickup(this.gameObject); //calls method in food script

                //sound
                FindObjectOfType<AudioManager>().Play("PickUpSound");

                //animation
                anim.SetTrigger("pickup");
        }

        #endregion


        //Cooldowns
        ThrowCool -= Time.deltaTime;
        EatCool -= Time.deltaTime;
        ThrowCool = Mathf.Clamp(ThrowCool, 0, .25f);
        EatCool = Mathf.Clamp(EatCool, 0, .25f);
    }

    //Collision --> pickup
    private void OnTriggerStay2D(Collider2D other)
    {
        /*
         * Prevent "Stealing" eggs, while also not registering it in object.
         */

        //upon collision with food
        if (other.tag == "Food")
        {
            //register other object as visitor.
            visitor = other.GetComponent<Food>();
            if (visitor.foodState == Food.FoodState.Held)
            {
                visitor = null;
            }
            else if (visitor.foodState == Food.FoodState.Shot)
            { 
                //if collide with food that was shot, take damage
                if (visitor.player != gameObject)
                {
                    //call damage method in stats manager script
                    stats.takeDamage(visitor.getType());
                    visitor.Smash(); //deletes food object

                    //HitSound
                    FindObjectOfType<AudioManager>().Play("HitSound");

                }
                visitor = null;
            }


        }
        else visitor = null;
    }

    //method to call eat health function in stats manager, delete object, and clear prisoner.
    public void UseFood()
    {
        if (triggers > .5 && EatCool == 0)
        {
            //Reset Cooldown
            EatCool = .25f;

            //adds health to stats
            stats.eatFood(prisoner.getType());

            prisoner.Smash(); //deletes food object


            //sound
            FindObjectOfType<AudioManager>().Play("EatSound");

            //animation
            anim.SetTrigger("eat");

            BailPrisoner(); //Bail after eat.
        }
    }

    //method to launch the food, and clear necessary local variables.
    public void CheckShoot()
    {

        if (triggers < -.5 && ThrowCool == 0)
        {
            //Reset Cooldown
            ThrowCool = .25f;
            
            float launchAngle = scriptref.RAngle;
            prisoner.Launched(launchAngle);


            //sound
            FindObjectOfType<AudioManager>().Play("ThrowSound");

            //animation
            anim.SetTrigger("throw");

            //get stats to increment GOthrows
            this.GetComponent<StatsManager>().CountThrow();

            //Only bail after throw.
            BailPrisoner();
        }
    }

    //Only this object should ever bail itself. otherwise, it runs into issues when picking up other objects.
    private Food BailPrisoner()
    {
        Food release = prisoner;
        prisoner = null;
        visitor = null;
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
     */
}