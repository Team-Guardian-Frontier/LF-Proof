using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    /*To-Do:
     * - Add state changes, and behaviors. (public methods)
     * 
     */

    public enum FoodType {
        Vegetable,
        Carbs,
        Proteins
    }

    //enum that dictates the state the instance is in
    public enum FoodState {
        None,
        Held,
        Shot
    }

    public FoodType foodType;
    public FoodState foodState;


    //Game Object, for "owner"
    private GameObject player;

    //positioning offset (for held)
    private Vector3 offset;

    //Launch fields
    public float maxSpeed;
    private float xspeed;
    private float yspeed;
    private Vector3 speedWagon;

    //Physics and Sprites
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Sprite foodSprite;
    private Texture2D foodTexture;
    private GameObject foodObject;
    private Rigidbody2D foodRigid;

    /**
     * Constructor that takes in food type, and a vector3 for position.
     */
    public Food(FoodType _foodType, Vector3 _initialPosition, int _count) {

        //instantiate and name the object
        foodObject = new GameObject(_foodType.ToString() + _count);

        
        foodObject.gameObject.tag = "Food";
        //setting food type
        foodType = _foodType;
        foodObject.transform.position = _initialPosition;

        //spawn set food state to none
        foodState = (FoodState)0;

        //set food sprite
        switch (foodType) {

            case FoodType.Carbs: {
                    // setup carb
                    foodTexture = Resources.Load<Texture2D>("sprites/bread");
                    Debug.Log("I am a bread!");
                    break;
                }

            case FoodType.Proteins: {
                    // setup proteins
                    foodTexture = Resources.Load<Texture2D>("sprites/egg");
                    Debug.Log("I am a Protein!");
                    break;
                }

            case FoodType.Vegetable: {
                    // setup vegetables
                    foodTexture = Resources.Load<Texture2D>("sprites/tomato");
                    Debug.Log("I am a vegetable!");
                    break;
                }

            default: {
                    Debug.Log("No foodType specified. Refusing to instantiate Food Object.");
                    break;
                }
        }

        //Sprite render
        spriteRenderer = foodObject.AddComponent<SpriteRenderer>();
        foodSprite = Sprite.Create(foodTexture,
                                   new Rect(0, 0, foodTexture.width, foodTexture.height),
                                   Vector2.zero);
        spriteRenderer.sprite = foodSprite;
        //Add Collider
        boxCollider = foodObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        /*add rigidbody2d, if needed
        foodRigid = (Rigidbody2D)foodObject.AddComponent(typeof(Rigidbody2D));
        foodRigid.gravityScale = 0;
        */

        //add Script
        foodObject.AddComponent(typeof(Food));

    }


    //Unity Events
    private void Start()
    {
        //set speed
        maxSpeed = .1f;
    }
    void Update()
    {
        switch (foodState)
        {
            case FoodState.Held:
                Held();
                break;
            case FoodState.Shot:
                Shot();
                break;
            default:
                break;

        }
    }

    //trigger events

    public void Pickup(GameObject other)
    {
        //pass ref to this object to player (for shooting and eating, and type.)

        player = other;
        foodState = FoodState.Held;


        offset = Vector3.zero; // this.transform.position - other.transform.position;

    }
    //call on player, when press trigger. (this instance already passed.)
    public void Launched(float angle)
    {
        //set speed and transforming
        //calculate xspeed and yspeed with trig, set to Vector3
        xspeed = Mathf.Cos(angle * Mathf.Deg2Rad) * maxSpeed;
        yspeed = Mathf.Sin(angle * Mathf.Deg2Rad) * maxSpeed;
        speedWagon = new Vector3(xspeed, yspeed);

        foodState = FoodState.Shot;


        Debug.Log("I was shot!" + foodState);


        
    }



    //utility
    private void Held()
    {
        this.transform.position = player.transform.position + offset;
    }

    private void Shot()
    {
        //move
        this.transform.Translate(speedWagon);
    }


}

