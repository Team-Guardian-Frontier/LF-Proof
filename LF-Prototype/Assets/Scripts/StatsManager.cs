using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import ui
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
    //temp header: this controls the player's stats like health and food groups.


    public Text playerText;
    public Text winText;

    public Food foodItem;

    //Public values available to set
    public int health; //health
    public int damage; //damage
    public int healing; //healing
    public int servingSize; // default food group amt added for each food pickup
    public int hungerD; //hunger damage
    public int GroupD; //damage from exceeding food groups.
    // maximum amount for each type
    public int maxVegetable;
    public int maxCarb;
    public int maxProtein;

    //set during play, counters
    private int totalH;
    private int carbCounter;
    private int proteinCounter;
    private int vegetableCounter;


    void Start () {


        //reset counters
        Debug.Log("I added food");
        vegetableCounter = 25;
        carbCounter = 25;
        proteinCounter = 25;

        //this is to set total health.
        totalH = health;
        Display();
        winText.text = "";

        /*DESIGN: these are the values you set.
        health = 100;
        damage = 25;
        healing = 25;
        serving size = 15;

        hungerD = 15;
        GroupD = 15;

        max groups
            veg65
            carb65
            protein65
        */
    }

    void Update () {

        Display();
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

    private void Display()
    {
        //don't forget the size of the text object!
        playerText.text = this.gameObject.name + ": " + health + "/" + totalH
                            + "\nVeggies: " + vegetableCounter + "/" + maxVegetable 
                            +"\nCarbs: " + carbCounter + "/" + maxCarb
                            +"\nProtein: " + proteinCounter + "/" + maxProtein;
                           
        if (health <= 0)
            Loss();

    }

    public void Loss()
    {
        Debug.Log("Is this...?");
        winText.text = "Game Over! You are Winner!";
        Destroy(gameObject);
    }
}
