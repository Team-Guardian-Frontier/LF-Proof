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

    public int GetCarbs() {
        return this.carbCounter;
    }

    public int GetProteins() {
        return this.proteinCounter;
    }

    public int GetFats() {
        return this.fatCounter;
    }

    public int GetFruits() {
        return this.fruitCounter;
    }

    public int GetVegetables() {
        return this.vegetableCounter;
    }
}
