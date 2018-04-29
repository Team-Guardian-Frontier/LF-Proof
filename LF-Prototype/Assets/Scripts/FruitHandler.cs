﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHandler : MonoBehaviour {


    //Calls StatsManager script
    private StatsManager stats;
    private Food food;

    //fruit object space


    // Use this for initialization
    void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            food = other.gameObject.GetComponent<Food>();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Bullet")
        {
            stats.takeDamage();
        }
        /*if Game Tag,
        Read type,
        set object to hold.

        Need to have a variable to hold the specific object.

        Shoot object, on button.

        */

    }


    public void UseFood()
    {
        Food.FoodType type = food.foodType;
        stats.eatFood(type);
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
     */
}
