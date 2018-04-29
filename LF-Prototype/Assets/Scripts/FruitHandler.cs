using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FruitHandler : MonoBehaviour {

    //Player Stats
    private float health;
    private float veggies;
    private float protein;
    private float grain;

    //fruit object space


	// Use this for initialization
	void Start () {
        //instantiate Stats
        health = 100;
        veggies = 0;
        protein = 0;
        grain = 0;

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        /*if Game Tag,
        Read type,
        set object to hold.

        Need to have a variable to hold the specific object.

        Shoot object, on button.

        */

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
