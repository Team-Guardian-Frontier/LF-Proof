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

    private FoodType foodType;
    public FoodState foodState;


    //Object for "Owner" player
    private GameObject player;

    //positioning offset (for held)
    private Vector3 offset;

    //Launch fields
    public float maxSpeed;
    private float xspeed;
    private float yspeed;
    private Vector3 speedWagon;

    //Physics and Sprites

    private SpriteRenderer spriteRenderer;
    private Sprite foodSprite;
    private Texture2D foodTexture;
    private Rigidbody2D foodRigid;
    //game object attatched
    private GameObject foodObject;

    //Add Collider


    /*add rigidbody2d, if needed
    foodRigid = (Rigidbody2D)foodObject.AddComponent(typeof(Rigidbody2D));
    foodRigid.gravityScale = 0;
    */

    //add Script ???What???





    //Unity Events
    private void Start()
    {
        //INITIALIZE
        //find the object this script is attatched to
        foodObject = gameObject;

        //mathf will pick even numbers on .5, may need your own math here.
        int foodNumber = Mathf.RoundToInt(Random.Range(0.0f, 2.0f));                  // Randomizes the FoodType
        foodType = (Food.FoodType)foodNumber;                                   // Sets the FoodType enum using the random number



       
        //generate location to check
        Vector2 spawnLocation = new Vector2(Random.Range(-6.75f, 6.75f), Random.Range(-2.25f, 2.25f));  // Sets the random location of food spawn within a certain area

        // check an area around the food to see if another food overlaps, if so then move the spawn location to another spot
        Collider2D foodInArea = Physics2D.OverlapCircle(spawnLocation, 1.0f);
        while (foodInArea != null)
        {
            spawnLocation = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-1.75f, 1.75f)); // checks a smaller area if the first location is too close
            foodInArea = null; // reset array
            foodInArea = Physics2D.OverlapCircle(spawnLocation, 1f); // check again
        }

        

        //set position
        foodObject.transform.position = spawnLocation;

        //add collider + rigidbody
        BoxCollider2D boxCollider = foodObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.offset = new Vector2(.16f, .16f);

        Rigidbody2D hardBody = foodObject.AddComponent<Rigidbody2D>();
        //make it so physics doesn't apply to these objects, but still detect collisions/use oncollisionenter
        hardBody.isKinematic = true;
        hardBody.useFullKinematicContacts = true;

        //DEBUG: say type
        Debug.Log("I am a " + foodType);

        //spawn set food state to none
        foodState = (FoodState)0;

        //SPRITE RENDERING
        //set food sprite
        switch (foodType)
        {

            case FoodType.Carbs:
                {
                    // setup carb
                    foodTexture = Resources.Load<Texture2D>("sprites/bread");
                    break;
                }

            case FoodType.Proteins:
                {
                    // setup proteins
                    foodTexture = Resources.Load<Texture2D>("sprites/egg");
                    break;
                }

            case FoodType.Vegetable:
                {
                    // setup vegetables
                    foodTexture = Resources.Load<Texture2D>("sprites/tomato");
                    break;
                }

            default:
                {
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

        //getting collider to match sprite
        boxCollider.size = foodSprite.bounds.size;

        //set speed
        maxSpeed = .1f;
        Debug.Log("Start, I am a " + foodType);
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

    //State Change calls (call from other scripts.

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

    //get method
    public FoodType getType()
    {
        return foodType;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject punch = other.gameObject;
        if (punch.CompareTag("Wall"))
            Destroy(this.gameObject);
        Debug.Log("Am I doing this Right?");
    }
    //oncollisionenter
    //delete the food here, not in the fruit handler.

}

