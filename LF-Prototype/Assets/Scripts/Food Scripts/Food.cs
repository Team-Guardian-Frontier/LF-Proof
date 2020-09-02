using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IPooledObject {

    #region Enums
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
    #endregion


    private FoodType foodType;
    public FoodState foodState; //NOTE: Still uses access on Fruithandler, change when you get the chance.

    //Sprites
    [SerializeField] private Sprite CarbSprite = null;
    [SerializeField] private Sprite MeatSprite = null;
    [SerializeField] private Sprite VegSprite = null;


    //Object for "Owner" player
    public GameObject player { get; private set; }

    #region  PRIVATE FIELDS
    //Obj Components
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D foodRigid;
    private BoxCollider2D foodBox;

    //Launch/Phys Fields
    public float maxSpeed = .1f;
    private Vector3 speedWagon; //current speed
    private Vector3 offset;     //positioning offset (for held)

    private AudioManager audioManager;
    private Sound flyClone;

    #endregion


    //Unity Events
    private void Awake()
    {
        //INITIALIZE 

        //Get and initialize Rigidbody, Boxcollider, and Sprite Renderer
        foodBox = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        foodRigid = gameObject.GetComponent<Rigidbody2D>();

        
        //ACCESS AUDIO MANAGER
        audioManager = GameManager.Instance.RegisterInjection<AudioManager>();
        //Sound Clone initialize
        flyClone = audioManager.PlayClone("FoodFly");
        flyClone.source.Stop();
        
    }

    //Pooling functionality
    public void OnObjectSpawn()
    {
        //spawn set food state to none
        foodState = FoodState.None;

        if (player != null)
        {
            Debug.Log("Spawn Error: A held food has Respawned");
            Smash();
        }

        //pick food number
        int foodNumber = Mathf.RoundToInt(Random.Range(0.0f, 2.0f));    //mathf will pick even numbers on .5, may need your own math here.
        foodType = (Food.FoodType)foodNumber;

        #region Sprites
        //set food sprite
        switch (foodType)
        {
            case FoodType.Carbs:
                {
                    spriteRenderer.sprite = CarbSprite;
                    break;
                }
            case FoodType.Proteins:
                {
                    spriteRenderer.sprite = MeatSprite;
                    break;
                }

            case FoodType.Vegetable:
                {
                    spriteRenderer.sprite = VegSprite;
                    break;
                }

            default:
                {
                    Debug.Log("No foodType specified. Refusing to instantiate Food Object.");
                    break;
                }
        }

        //getting collider to match sprite
        foodBox.size = spriteRenderer.size;

        #endregion

        #region Randomizing Location

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
        gameObject.transform.position = spawnLocation;

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

    #region State Functions

    private void Held() //Held State
    {
        this.transform.position = player.transform.position + offset;
    }

    private void Shot() //Moving state
    {
        //move
        this.transform.Translate(speedWagon);

    }

    #endregion



    #region Public Methods

    //get methods
    public FoodType getType()
    {
        return foodType;
    }
    public FoodState getState()
    {
        return foodState;
    }


    public void Pickup(GameObject other)
    {
        //pass ref to this object to player (for shooting and eating, and type.)

        player = other;
        foodState = FoodState.Held;


        offset = new Vector2(.16f, .16f); // this.transform.position - other.transform.position;
    }

    //call on player, when press trigger. (this instance already passed.)
    public void Launched(float angle)
    {
        foodState = FoodState.Shot;

        //set speed and transforming
        //calculate xspeed and yspeed with trig, set to Vector3
        float xspeed = Mathf.Cos(angle * Mathf.Deg2Rad) * maxSpeed;
        float yspeed = Mathf.Sin(angle * Mathf.Deg2Rad) * maxSpeed;
        speedWagon = new Vector3(xspeed, yspeed);


        //sound
        flyClone.source.Play();
    }

    public void Smash() //"Destroys" food object
    {
        player = null;

        foodState = FoodState.None; //reset the player, reset the food's player, reset the foodstate.

        this.gameObject.SetActive(false);

        flyClone.source.Stop(); // this Does work
        //sets up nicely for possible pooling later on
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        GameObject punch = other.gameObject;

        //Just make it so it doesn't die on walls while held.
        if (punch.CompareTag("Wall") && foodState != FoodState.Held)
        {
            this.gameObject.SetActive(false);

            Smash();
            audioManager.Play("SplatSound");
        }

    }
    //oncollisionenter
    //delete the food here, not in the fruit handler.

}

