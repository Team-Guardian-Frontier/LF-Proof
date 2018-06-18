using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Author: Irvin Naylor
 * Last Change: 6/8/18
 * Object: Player
 * What script does: Controls projectile firepoint and fire rate
 * List of Fields: 
 *      - fireRate: controls fire rate 
 *      - TTF: Time until fire, controls the delay in between shots
 *      - firePoint: area where the bullet is firing (set to the mouse position)
 *      - bullet: attributed to a new bullet object
 * List of Methods:
 *      - Update(): Updates every second, determines if player is ok to fire again
 *      - Shoot(): simply instantiates a new bullet object and sets its path to firePoint's location
 * Notes:
 * 
 */

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
