using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    //attached to eventsystem

    private Food food;
    private int foodNumber;
    private Food.FoodType spawnType;
    private int foodCount = 0;
    private int maxFood = 10; // may change for "food frenzy"
    public GameObject FoodPrefab;

    FoodPooler foodPooler;
    
	// Use this for initialization
	void Start () {
        foodPooler = FoodPooler.Instance;
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
            GameObject foodObject = foodPooler.SpawnFromPoolActive("food");

            if (foodObject != null)
            {
                foodObject.gameObject.tag = "Food";

                foodCount += 1;
            }
            //run instantiate, since spawn food script already does all the math. For some reason. (Also, add interace for spawn in new locations.)


            //For some reason, food spawned keeps dying on the wall.




        }
        
    }

    void despawnFood()
    {
        //find all objects with food tag
        GameObject[] hitList = GameObject.FindGameObjectsWithTag("Food");
        //interate through to check and destroy
        for (int i = 0; i < hitList.Length; i++)
        {
            Food foodScript = hitList[i].GetComponent<Food>();

            //if the object is not shot or held, destroy it.
            if (foodScript.foodState == Food.FoodState.None)
            {

                hitList[i].SetActive(false);
            }
        }
    }
}
