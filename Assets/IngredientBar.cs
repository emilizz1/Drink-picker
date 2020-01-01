using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientBar : MonoBehaviour
{
    [SerializeField] Text ingredientName;
    [SerializeField] Sprite selectedSprite, unselectedSprite;
    [SerializeField] Image selectImage;

    Drink.Ingredient thisIngredient;

    private void Start()
    {
        if (DrinkPicker.instance.currentlySelectedIngredient.Contains(thisIngredient))
        {
            selectImage.sprite = selectedSprite;
        }
    }

    public void PrepareBar(Drink.Ingredient ingredient)
    {
        thisIngredient = ingredient;
        ingredientName.text = ingredient.ToString();
    }

    public void Pressed()
    {
        if (selectImage.sprite == selectedSprite)
        {
            selectImage.sprite = unselectedSprite;
            DrinkPicker.instance.currentlySelectedIngredient.Remove(thisIngredient);
        }
        else
        {
            selectImage.sprite = selectedSprite;
            DrinkPicker.instance.currentlySelectedIngredient.Add(thisIngredient);
        }
    }
}
