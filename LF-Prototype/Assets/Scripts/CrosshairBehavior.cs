using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is for crosshair
//Gets rid of mouse cursor while replacing it with custom crosshair

public class CrosshairBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Input.mousePosition;
        //still a WIP for now
	}
}
