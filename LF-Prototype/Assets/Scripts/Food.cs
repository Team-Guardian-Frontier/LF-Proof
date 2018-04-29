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

    //game object field for being held
    private GameObject player;

    //positioning offset (for held)
    private Vector3 offset;

    //Launch fields
    private float lAngle;
    public float maxSpeed;
    private float xspeed;
    private float yspeed;
    private Vector3 speedWagon;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Sprite foodSprite;
    private Texture2D foodTexture;
    private GameObject foodObject;

    /**
     * Constructor that takes in food type, and a vector3 for position.
     */
    public Food(FoodType _foodType, Vector3 _initialPosition) {

        foodObject = new GameObject(_foodType.ToString());

        foodObject.gameObject.tag = "Food";
        foodType = _foodType;
        foodObject.transform.position = _initialPosition;

        //spawn set food state to none
        foodState = (FoodState)0;

        switch (foodType) {

            case FoodType.Carbs: {
                    // setup carb
                    foodTexture = Resources.Load<Texture2D>("sprites/bread");
                    break;
                }

            case FoodType.Proteins: {
                    // setup proteins
                    foodTexture = Resources.Load<Texture2D>("sprites/egg");
                    break;
                }

            case FoodType.Vegetable: {
                    // setup vegetables
                    foodTexture = Resources.Load<Texture2D>("sprites/vegetable");
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

        boxCollider = foodObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

    }



    //Unity Events
    void Update()
    {
        switch(foodState)
        {
            case FoodState.Held:
                Held();
                break;
            case FoodState.Shot:
                break;
            default:
                break;
 
        }
    }

    //trigger events
    private void OnCollisionEnter2D(Collision2D other)
    {
        //pass ref to this object to player (for shooting and eating, and type.)

        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            foodState = FoodState.Held;
            Destroy(gameObject);
            offset = this.transform.position - other.transform.position;
        }
        
        
    }
        //call on player, when press trigger. (this instance already passed.)
    public void Launched(float angle)
    {
        //set speed and transforming
            //calculate xspeed and yspeed with trig, set to Vector3
        xspeed = Mathf.Cos(angle) * Mathf.Rad2Deg * maxSpeed;
        yspeed = Mathf.Sin(angle) * Mathf.Rad2Deg * maxSpeed;
        speedWagon = new Vector3(xspeed, yspeed);

        foodState = FoodState.Shot;
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

