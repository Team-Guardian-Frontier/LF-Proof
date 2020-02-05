using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPooledObject {
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
    private BoxCollider2D foodBox;
    //game object attatched
    private GameObject foodObject;

    [SerializeField]
    private Sound flyClone;


    //Add Collider


    /*add rigidbody2d, if needed
    foodRigid = (Rigidbody2D)foodObject.AddComponent(typeof(Rigidbody2D));
    foodRigid.gravityScale = 0;
    */

    //add Script ???What???



    

    //Unity Events
    private void Awake()
    {
        #region initialize 
        //INITIALIZE
        //find the object this script is attatched to
        foodObject = gameObject; 


        //Get and initialize Rigidbody, Boxcollider, and Sprite Renderer
        foodBox = foodObject.GetComponent<BoxCollider2D>();
        if (foodBox != null)
        { }
            else
        { foodBox = foodObject.AddComponent<BoxCollider2D>(); }

        foodBox.isTrigger = true;
        foodBox.offset = new Vector2(.16f, .16f);
        foodBox.edgeRadius = 0;
        //MAKE SURE EDGE RADIUS IS 0. Who in their right mind would use this on a boxcollider?


        foodRigid = foodObject.GetComponent<Rigidbody2D>();
        if (foodRigid != null)
        { }
        else
        { foodRigid = foodObject.AddComponent<Rigidbody2D>(); }
        //make it so physics doesn't apply to these objects, but still detect collisions/use oncollisionenter
        foodRigid.isKinematic = true;
        foodRigid.useFullKinematicContacts = true;

        //Sprite renderer initialize 
        spriteRenderer = foodObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        { }
        else
        { spriteRenderer = foodObject.AddComponent<SpriteRenderer>(); }

        //set speed
        maxSpeed = .1f;

        //sound clone initialize.
        flyClone = FindObjectOfType<AudioManager>().PlayClone("FoodFly");
        flyClone.source.Stop();

        #endregion


    }

    //Pooling functionality
    public void OnObjectSpawn()
    {
        //spawn set food state to none
        foodState = FoodState.None;

        if (player != null)
        {
            FruitHandler bailer = player.GetComponent<FruitHandler>();
            bailer.BailPrisoner();
            player = null;
        }

        //clear up prisoner

        #region OnSpawn

        //mathf will pick even numbers on .5, may need your own math here.
        int foodNumber = Mathf.RoundToInt(Random.Range(0.0f, 2.0f));                  // Randomizes the FoodType
        foodType = (Food.FoodType)foodNumber;                                   // Sets the FoodType enum using the random number

        //DEBUG: say type
        //Debug.Log("I am a " + foodType);

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
        foodSprite = Sprite.Create(foodTexture,
                                   new Rect(0, 0, foodTexture.width, foodTexture.height),
                                   Vector2.zero);
        spriteRenderer.sprite = foodSprite;

        //getting collider to match sprite
        foodBox.size = spriteRenderer.size;
        //foodBox.offset = new Vector2((spriteRenderer.size.x / 2), (spriteRenderer.size.y / 2));  Is Implied, with all our current sprites.


        //generate location to check
        Vector2 spawnLocation = new Vector2(Random.Range(-6.75f, 6.75f), Random.Range(-2.25f, 2.25f));  // Sets the random location of food spawn within a certain area

        // check an area around the food to see if another food overlaps, if so then move the spawn location to another spot
        Collider2D foodInArea = Physics2D.OverlapCircle(spawnLocation, 1.0f);
        while (foodInArea != null)
        {
            spawnLocation = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-1.75f, 1.75f)); // checks a smaller area if the first location is too close
            foodInArea = null; // reset array
            foodInArea = Physics2D.OverlapCircle(spawnLocation, 1f); // check again

            //Debug.Log("Foodinarea:" + foodInArea);

        }

        //set position
        foodObject.transform.position = spawnLocation;
        //Debug.Log("I Shmoove to " + spawnLocation);


        #endregion
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


        //sound

        flyClone.source.Play();

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
        //do not put sound here, this is an update/state script!

    }

    public void Smash()
    {
        this.gameObject.SetActive(false);
        

        flyClone.source.Stop(); // this Does work
        //sets up nicely for possible pooling later on

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject punch = other.gameObject;

        //Just make it so it doesn't die on walls while held.
        if (punch.CompareTag("Wall") && foodState != FoodState.Held)
        {
            this.gameObject.SetActive(false);
            Debug.Log("I died on a wall at: " + foodObject.transform.position);

            Smash();
            FindObjectOfType<AudioManager>().Play("SplatSound");
        }

    }
    //oncollisionenter
    //delete the food here, not in the fruit handler.

}

