using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2FruitHandler : MonoBehaviour
{


    //Calls StatsManager script
    private StatsManager stats;


    //held food object
    private Food prisoner;
    private GameObject tempCast;

    //trigger inputs
    private float p2ltrigger;
    private float p2rtrigger;



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
        p2ltrigger = Input.GetAxis("ltrig2");
        p2rtrigger = Input.GetAxis("rtrig2");



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


            if (prisoner == null)
            {
                string pname = other.gameObject.name;
                tempCast = GameObject.Find(pname);
                prisoner = (Food)tempCast.GetComponent(typeof(Food));
                if (prisoner.foodState == Food.FoodState.Shot)
                {
                    stats.takeDamage();
                    // add food count for damage
                    Destroy(other.gameObject);
                    
                }
                else if(prisoner.foodState != Food.FoodState.Held)
                    prisoner.Pickup(this.gameObject);

            }

        }

    }


    public void UseFood()
    {
        if (p2ltrigger > .5)
        {
            stats.eatFood(prisoner.foodType);
            Destroy(tempCast);

        }
    }

    public void CheckShoot()
    {
        if (p2rtrigger > .5)
        {
            GameObject player = GameObject.Find("Player2");
            P2MousePos scriptref = (P2MousePos)player.GetComponent(typeof(P2MousePos));
            float launchAngle = scriptref.RAngle;
            prisoner.Launched(launchAngle);

            prisoner = null;
            tempCast = null;
        }
    }
}
