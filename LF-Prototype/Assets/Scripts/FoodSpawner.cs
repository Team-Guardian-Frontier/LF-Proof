using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    private Food food;
    private int foodNumber;
    private Food.FoodType spawnType;
    private int foodCount = 0;
    private int maxFood = 10; // may change for "food frenzy"
 
    private Vector2 spawnLocation;
    //DILAPIDATED, have an entirely different timer system 

	// Use this for initialization
	void Start () {
        SpawnFood();
    }
	
	// Update is called once per frame
	void Update () {

    }

    //food Respawn (spawn and respawn in the same command
    public void RespawnFood()
    {
        despawnFood();
        SpawnFood();
    }


    // Spawns food at random locations at a time interval based on the "timer" variable
    // An int timer spawns the food in rows and columns, great for specifying static spawn points (tends to spawn in the same spots)
    // A float timer makes the location completely random
    public void SpawnFood()
    {
        //reset food count
        foodCount = 0;
        while (foodCount < maxFood)
        {
            //instance of object
            //instantiate and name the gameobject
            GameObject foodObject = new GameObject("food" + foodCount);

            foodObject.gameObject.tag = "Food";

            // attatch scripts
            foodObject.AddComponent<Food>();

            //attatch collider after location set.




            foodCount += 1;
            Debug.Log("Created food at " + Time.time);
        }
    }

    void despawnFood()
    {
        //find all objects with food tag
        GameObject[] hitList = GameObject.FindGameObjectsWithTag("food");
        //interate through to check and destroy
        for (int i = 0; i < hitList.Length; i++)
        {
            Food foodScript = hitList[i].GetComponent<Food>();

            //if the object is not shot or held, destroy it.
            if (foodScript.foodState == (Food.FoodState)0) //Foodstate 0 = none
            {
                Destroy(hitList[i]);
            }
        }
    }
}
