using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//import ui
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
    //temp header: this controls the player's stats like health and food groups.

    //NOTE: be wary. getCarbs and GetGOcarbs are very different methods.


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

    //Game Over counters
    private int GOeaten =0;
    private int GOthrows =0;
    private int GOhitten =0;

    private int GOcarbs =0;
    private int GOprotein =0;
    private int GOveggies =0;


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

    public int GetGOeaten() { return GOeaten; }
    public int GetGOthrows() { return GOthrows; }
    public int GetGOhitten() { return GOhitten; }

    public int GetGOcarbs() { return GOcarbs; }
    public int GetGOprotein() { return GOprotein; }
    public int GetGOveggies() { return GOveggies; }
        //NOTE: be wary. getCarbs and GetGOcarbs are very different methods.




    //Actions and stuff
    public void eatFood(Food.FoodType _Type)
    {
        //adds health, applies food group method
        health += healing;
        FoodGroup(_Type);

        //increment GOeaten
        GOeaten++;
    }

    public void CountThrow()
    {
        GOthrows++;
    }

    public void FoodGroup(Food.FoodType _foodType)
    {
        //Adds the appropriate amt to group, and then incurs appropriate penalty.
        
        switch (_foodType)
        {
            case Food.FoodType.Vegetable:
                {
                    vegetableCounter += servingSize;

                    //increment GOveggies
                    GOveggies++;

                    if (vegetableCounter > maxVegetable)
                    {
                        health -= GroupD; //currently, set so that if any counter is over the max, then incur damage, so less healing

                        //sound
                        FindObjectOfType<AudioManager>().Play("FoodBarSound");
                    }

                    break;
                }
            case Food.FoodType.Carbs:
                {
                    carbCounter += servingSize;

                    //increment GOcarbs
                    GOcarbs++;

                    if (carbCounter > maxCarb)
                    {
                        health -= GroupD;

                        //sound
                        FindObjectOfType<AudioManager>().Play("FoodBarSound");
                    }
                    break;
                }
            case Food.FoodType.Proteins:
                {
                    proteinCounter += servingSize;

                    //increment GOprotein
                    GOprotein++;

                    if (proteinCounter > maxProtein)
                    {
                        health -= GroupD;

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
        FoodGroup(_Type);

        //increment GOhitten
        GOhitten++;
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
        //gameObject.SetActive(false);
    }
}
