using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import ui
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
    public Text playerText;
    public Text winText;


    public int health;
    private int totalH;
    private int carbCounter;
    private int proteinCounter;
    private int vegetableCounter;

    // maximum amount for each type
    public int maxVegetable;
    public int maxCarb;
    public int maxProtein;

    public Food foodItem;
    public int servingSize; // default amount added for each food pickup

    void Start () {
        health = 100; // base health
        vegetableCounter = 0;
        carbCounter = 0;
        proteinCounter = 0;

        //this is to set total health.
        totalH = health;
        DisplayHealth();
        winText.text = "";
	}
	
	void Update () {
        DisplayHealth();
	}

    public int GetVegetables() {
        return this.vegetableCounter;
    }

    public int GetCarbs() {
        return this.carbCounter;
    }

    public int GetProteins() {
        return this.proteinCounter;
    }

    public void eatFood(Food.FoodType foodType)
    {
        health += servingSize;

        /*
        int penalty;
        switch (foodType)
        {
            case Food.FoodType.Vegetable:
                {
                    vegetableCounter += servingSize;
                    if (vegetableCounter > maxVegetable)
                    {
                        penalty = (vegetableCounter - maxVegetable);  // penalty is taken from health based on overflow
                        health -= penalty;
                    }
                    break;
                }
            case Food.FoodType.Carbs:
                {
                    carbCounter += servingSize;
                    if (carbCounter > maxCarb)
                    {
                        penalty = (carbCounter - maxCarb);  // penalty is taken from health based on overflow
                        health -= penalty;
                    }
                    break;
                }
            case Food.FoodType.Proteins:
                {
                    proteinCounter += servingSize;
                    if (proteinCounter > maxProtein)
                    {
                        penalty = (proteinCounter - maxProtein);  // penalty is taken from health based on overflow
                        health -= penalty;
                    }
                    break;
                }
            default:
                Debug.Log("No foodType specified. Cannot add food to counter.");
                break;
        }
        */
    }

    public void takeDamage()
    {
        health -= servingSize;
        
    }

    private void DisplayHealth()
    {
        playerText.text = this.gameObject.name + ": " + health + "/" + totalH;
        if (health <= 0)
            Loss();

    }

    public void Loss()
    {
        winText.text = "Game Over! You are Winner!";
        Destroy(gameObject);
    }
}
