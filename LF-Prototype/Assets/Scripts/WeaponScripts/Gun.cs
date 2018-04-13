using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float fireRate = 3; //dependant on gun
    public float damage = 8; //default damage value
    float TTF = 0; //Time until Fire

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > TTF)
        {
            TTF = Time.time + 1 / fireRate;
            Shoot();
        }
        

    }

    void Shoot()
    {
        //Shooting placeholder
        Debug.Log("Shooting");
    }

}
