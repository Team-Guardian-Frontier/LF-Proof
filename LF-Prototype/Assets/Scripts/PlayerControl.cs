using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    private Rigidbody2D myRigidbody;

    private Vector3 moveInput;


	
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	
	void Update () {
		if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector2.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector2.up * moveSpeed);
        }


    }
}
