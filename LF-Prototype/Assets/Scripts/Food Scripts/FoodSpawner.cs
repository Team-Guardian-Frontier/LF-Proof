using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    //Private values
    private Food food;
    private int foodNumber;
    private Food.FoodType spawnType;
    private int maxFood = 10; // may change for "food frenzy"

    //public dependencies
    public FoodPooler foodPooler;

    //food Respawn (spawn and respawn in the same command
    public void RespawnFood()
    {
        despawnFood();
        SpawnFood();
    }

    public void SpawnFood()
    {
        //reset food count
        int foodCount = 0;
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
            if (foodScript.getState() == Food.FoodState.None)
            {

                hitList[i].SetActive(false);
            }
        }
    }
}
