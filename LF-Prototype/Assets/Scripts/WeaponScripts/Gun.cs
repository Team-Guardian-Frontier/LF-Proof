using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float fireRate = 3; //dependant on gun
    float TTF = 0; //Time until Fire
    public Transform firePoint;
    public GameObject bullet;


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
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

}
