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

    public Slider healthBar;
    public Slider carbBar;
    public Slider proteinBar;
    public Slider vegtableBar;


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
        healthBar.value = health;
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
                    vegtableBar.value = vegetableCounter;

                    if (vegetableCounter > maxVegetable)
                    {
                        health -= GroupD; //currently, set so that if any counter is over the max, then incur damage, so less healing
                        healthBar.value = health;

                        //sound
                        FindObjectOfType<AudioManager>().Play("FoodBarSound");
                    }

                    break;
                }
            case Food.FoodType.Carbs:
                {
                    carbCounter += servingSize;
                    carbBar.value = carbCounter;
                    if (carbCounter > maxCarb)
                    {
                        health -= GroupD;
                        healthBar.value = health;

                        //sound
                        FindObjectOfType<AudioManager>().Play("FoodBarSound");
                    }
                    break;
                }
            case Food.FoodType.Proteins:
                {
                    proteinCounter += servingSize;
                    proteinBar.value = proteinCounter;
                    if (proteinCounter > maxProtein)
                    {
                        health -= GroupD;
                        healthBar.value = health;

                        //sound
                        FindObjectOfType<AudioManager>().Play("FoodBarSound");
                    }
                    break;
                }
            default:
                Debug.Log("Invalid foodType. Cannot add food to counter.");
                break;
        }
        
    }

    public void takeDamage(Food.FoodType _Type)
    {
        //adds damage, applies food group method.
        health -= damage;
        healthBar.value = health;
        FoodGroup(_Type);
    }

    public void hungerDamage()
    {
        //hunger damage method
        health -= hungerD;
        healthBar.value = health;

    }

    public void groupReset()
    {
        vegetableCounter = 0;
        vegtableBar.value = vegetableCounter;
        carbCounter = 0;
        carbBar.value = carbCounter;
        proteinCounter = 0;
        proteinBar.value = proteinCounter;
    }

    private void Display()
    {
        //don't forget the size of the text object!
        playerText.text = this.gameObject.name + ": " + health + "/" + totalH
                            + "\nVeggies: " + vegetableCounter + "/" + maxVegetable 
                            +"\nCarbs: " + carbCounter + "/" + maxCarb
                            +"\nProtein: " + proteinCounter + "/" + maxProtein;
                       
        //death
        if (health <= 0)
        {
            Loss();

            //sound
            FindObjectOfType<AudioManager>().Play("DeathSound");
        }
            

    }

    public void Loss()
    {
        Debug.Log("Is this...?");
        winText.text = "Game Over! You are Winner!";
        Destroy(gameObject);
    }
}
