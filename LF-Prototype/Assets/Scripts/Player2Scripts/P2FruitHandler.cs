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


        if (other.tag == "Food")
        {
            Debug.Log("Collision Detected");

            //find a way to check state of other, without taking prisoner.
            string pname = other.gameObject.name;
            tempCast = GameObject.Find(pname);
            visitor = (Food)tempCast.GetComponent(typeof(Food));



            if (visitor.foodState == Food.FoodState.Shot)
            {
                stats.takeDamage();
                // add food count for damage
                Destroy(other.gameObject);

            }
            else if (prisoner == null)
            {
                prisoner = visitor;
                Debug.Log("prisonerstate" + prisoner.foodState);

                if (prisoner.foodState == Food.FoodState.Held)
                {
                    //bump
                    prisoner = null;
                    tempCast = null;
                }
                else
                {
                    prisoner.Pickup(this.gameObject);

                }

            }
                

        }

    }


    public void UseFood()
    {
        if (triggers > .5)
        {
            stats.eatFood(prisoner.foodType);
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
