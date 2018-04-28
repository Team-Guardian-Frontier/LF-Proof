using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    private int carbCounter;
    private int proteinCounter;
    private int fatCounter;
    private int fruitCounter;
    private int vegetableCounter;

	void Start () {
        carbCounter = 0;
        proteinCounter = 0;
        fatCounter = 0;
        fruitCounter = 0;
        vegetableCounter = 0;
	}
	
	void Update () {
		
	}

    public int getCarbCounter() {
        return this.carbCounter;
    }

    public int getProteinCounter() {
        return this.proteinCounter;
    }

    public int getFatCounter() {
        return this.fatCounter;
    }

    public int getFruitCounter() {
        return this.fruitCounter;
    }

    public int getVegetableCounter() {
        return this.vegetableCounter;
    }
}
