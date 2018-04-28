using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public enum FoodType {
        Fruit,
        Vegetable,
        Carbs,
        Fats,
        Proteins
    }

    public FoodType foodType;

    private SpriteRenderer spriteRenderer;
    private Sprite foodSprite;
    private Texture2D foodTexture;
    private GameObject foodObject;

    public Food(FoodType _foodType, Vector3 _initialPosition) {
        foodObject = new GameObject("Fruit");

        foodType = _foodType;
        foodObject.transform.position = _initialPosition;

        switch (foodType) {
            case FoodType.Fruit: {
                    // setup fruit
                    foodTexture = Resources.Load<Texture2D>("sprites/orange");
                    break;
                }

            case FoodType.Carbs: {
                    // setup carb
                    foodTexture = Resources.Load<Texture2D>("sprites/bread");
                    break;
                }

            case FoodType.Fats: {
                    // setup fats
                    foodTexture = Resources.Load<Texture2D>("sprites/fat");
                    break;
                }

            case FoodType.Proteins: {
                    // setup proteins
                    foodTexture = Resources.Load<Texture2D>("sprites/protein");
                    break;
                }

            case FoodType.Vegetable: {
                    // setup vegetables
                    foodTexture = Resources.Load<Texture2D>("sprites/vegetable");
                    break;
                }

            default: {
                    Debug.Log("No FoodType specified. Refusing to instantiate Food Object.");
                    break;
                }
        }

        spriteRenderer = foodObject.AddComponent<SpriteRenderer>();
        foodSprite = Sprite.Create(foodTexture,
                                   new Rect(0, 0, foodTexture.width, foodTexture.height),
                                   Vector2.zero);
        spriteRenderer.sprite = foodSprite;
    }
}
