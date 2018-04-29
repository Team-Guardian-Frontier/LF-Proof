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
    private float spawnTimer = 2.0f;    
	// Use this for initialization
	void Start () {
        Spawner();
    }
	
	// Update is called once per frame
	void Update () {

    }

    // Spawns food at random locations at a time interval based on the "timer" variable
    // An int timer spawns the food in rows and columns, great for specifying static spawn points (tends to spawn in the same spots)
    // A float timer makes the location completely random
    void Spawner()
    {
        while (foodCount < maxFood)
        {

            foodNumber = 2; // Set to Protein for debugging, delete this and uncomment the next line for random food type
                            //foodNumber = Random.Range(0, 2);                                                  // Randomizes the FoodType
            spawnType = (Food.FoodType)foodNumber;                                                  // Sets the FoodType using the random number

            spawnLocation = new Vector2(Random.Range(-6.75f, 6.75f), Random.Range(-2.25f, 2.25f));  // Sets the random location of food spawn within a certain area

            // check an area around the food to see if another food is nearby, if so then move the spawn location to another spot
            Collider2D foodInArea = Physics2D.OverlapCircle(spawnLocation, 1.0f);
            while (foodInArea != null)
            {
                spawnLocation = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-1.75f, 1.75f)); // checks a smaller area if the first location is too close
                foodInArea = null; // reset array
                foodInArea = Physics2D.OverlapCircle(spawnLocation, 1f); // check again
            }
            // spawn the food
            food = new Food(spawnType, spawnLocation, foodCount);
            foodCount += 1;
            Debug.Log("Created food: '" + food.foodType + "' at " + Time.time);
        }
    }
}
