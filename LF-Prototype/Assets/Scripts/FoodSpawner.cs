using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    private Food food;
    private int foodNumber;
    private Food.FoodType spawnType;
    private int foodCount = 0;
    private int maxFood = 10; // may change for "food frenzy"
    private bool spawnAvailable = true; // determines whether food can spawn
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
            foodNumber = 0; // Set to Fruit for debugging
                            //foodNumber = Random.Range(0, 4);                                                  // Randomizes the FoodType
            spawnType = (Food.FoodType)foodNumber;                                              // Sets the FoodType using the random number
            spawnLocation = new Vector2(Random.Range(-4.8f, 4.8f), Random.Range(-2.5f, 2.5f));  // Sets the random location of food spawn within a certain area

            // check an area around the food to see if another food is nearby, if so then move the spawn location to another spot
            Collider2D foodInArea = Physics2D.OverlapCircle(spawnLocation, 1.0f);
            while (foodInArea != null)
            {
                spawnLocation = new Vector2(Random.Range(-3.0f, 3.0f), Random.Range(-1.5f, 1.5f)); // checks a smaller area if the first location is too close
                foodInArea = null; // reset array
                foodInArea = Physics2D.OverlapCircle(spawnLocation, 1f); // check again
            }
            // spawn the food
            food = new Food(spawnType, spawnLocation);
            foodCount += 1;
            Debug.Log("Created food: '" + food.foodType + "' at " + Time.time);
        }
    }
}
