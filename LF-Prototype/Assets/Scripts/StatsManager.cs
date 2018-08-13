using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import ui
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
    //temp header: this controls the player's stats like health and food groups.


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
    public int damage; //damage
    public int healing; //healing
    public int hungerD; //hunger damage
    public int GroupD; //damage from exceeding food groups.
    public int servingSize; // default food group amt added for each food pickup
    

    void Start () {
        health = 100; // base health
        vegetableCounter = 0;
        carbCounter = 0;
        proteinCounter = 0;

        //this is to set total health.
        totalH = health;
        DisplayHealth();
        winText.text = "";

        //DEBUG: made these equal for testing purposes
        damage = servingSize;
        healing = servingSize;
        hungerD = damage;
        GroupD = hungerD;
    }

    void Update () {
        DisplayHealth();
	}


    //get methods
    public int GetVegetables() {
        return this.vegetableCounter;
    }

    public int GetCarbs() {
        return this.carbCounter;
    }

    public int GetProteins() {
        return this.proteinCounter;
    }

    public void eatFood(Food.FoodType _Type)
    {
        //adds health, applies food group method
        health += healing;
        FoodGroup(_Type);
    }

    public void FoodGroup(Food.FoodType _foodType)
    {
        //Adds the appropriate amt to group, and then incurs appropriate penalty.
        
        switch (_foodType)
        {
            case Food.FoodType.Vegetable:
                {
                    vegetableCounter += servingSize;
                    if (vegetableCounter > maxVegetable)
                    {
                        health -= GroupD; //currently, set so that if any counter is over the max, then incur damage, so less healing
                    }
                    break;
                }
            case Food.FoodType.Carbs:
                {
                    carbCounter += servingSize;
                    if (carbCounter > maxCarb)
                    {
                        health -= GroupD;
                    }
                    break;
                }
            case Food.FoodType.Proteins:
                {
                    proteinCounter += servingSize;
                    if (proteinCounter > maxProtein)
                    {
                        health -= GroupD;
                    }
                    break;
                }
            default:
                Debug.Log("No foodType specified. Cannot add food to counter.");
                break;
        }
        
    }

    public void takeDamage(Food.FoodType _Type)
    {
        //adds damage, applies food group method.
        health -= damage;
        FoodGroup(_Type);
    }

    public void hungerDamage()
    {
        //hunger damage method
        health -= hungerD;

    }

    public void groupReset()
    {
        vegetableCounter = 0;
        carbCounter = 0;
        proteinCounter = 0;
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
