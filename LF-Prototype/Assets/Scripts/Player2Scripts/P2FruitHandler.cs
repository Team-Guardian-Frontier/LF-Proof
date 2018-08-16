using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2FruitHandler : MonoBehaviour
{


    //Calls StatsManager script
    private StatsManager stats;


    //held food object
    private Food visitor;
    private Food prisoner;
    private GameObject tempCast;

    //trigger inputs
    private float triggers;



    // Use this for initialization
    void Start()
    {
        //load stats
        stats = (StatsManager)this.gameObject.GetComponent(typeof(StatsManager));

    }

    // Update is called once per frame
    void Update()
    {
        //player 2 triggers, ltrig2, rtrig2
        triggers = Input.GetAxis("triggers2");
        



        if (prisoner != null)
        {
            UseFood();
            CheckShoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*
        * Prevent "Stealing" eggs, while also not registering it in object.
        */

        if (other.tag == "Food")
        {


            //find a way to check state of other, without taking prisoner.
            string pname = other.gameObject.name;
            tempCast = GameObject.Find(pname);
            //register other object as visitor
            visitor = (Food)tempCast.GetComponent(typeof(Food));


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
                //set it so taht you now have a prisoner %possible optimization% see p1 fruit
                prisoner = visitor;
                    //DEBUG: tells you state of prisoner and type
                Debug.Log("prisonerstate" + prisoner.foodState);
                Debug.Log("prisonerType" + prisoner.getType());
                Debug.Log("PrisonerName" + prisoner.name);

                if (prisoner.foodState == Food.FoodState.Held)
                {
                    //clear prisoner details if you bump into other's
                    prisoner = null;
                    tempCast = null;
                }
                else
                {
                    //physically capture the object (it now follows)
                    prisoner.Pickup(this.gameObject);

                }

            }
                

        }

    }


    public void UseFood()
    {
        if (triggers > .5)
        {
            stats.eatFood(prisoner.getType());
            Destroy(tempCast);
            prisoner = null;
            tempCast = null;

        }
    }

    public void CheckShoot()
    {
        if (triggers < -.5)
        {
            GameObject player = GameObject.Find("Player2");
            P2MousePos scriptref = (P2MousePos)player.GetComponent(typeof(P2MousePos));

            float launchAngle = scriptref.RAngle2;
            prisoner.Launched(launchAngle);

            prisoner = null;
            tempCast = null;
        }
    }
}
