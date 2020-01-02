using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsIngredientBar : MonoBehaviour
{
    [SerializeField] Text ingredientName;
    [SerializeField] Sprite selectedSprite, unselectedSprite;
    [SerializeField] Image selectImage;

    Shot.Ingredient thisIngredient;

    private void Start()
    {
        if (ShotsPicker.instance.currentlySelectedIngredient.Contains(thisIngredient))
        {
            selectImage.sprite = selectedSprite;
        }
    }

    public void PrepareBar(Shot.Ingredient ingredient)
    {
        thisIngredient = ingredient;
        ingredientName.text = ingredient.ToString();
    }

    public void Pressed()
    {
        if (selectImage.sprite == selectedSprite)
        {
            selectImage.sprite = unselectedSprite;
            ShotsPicker.instance.currentlySelectedIngredient.Remove(thisIngredient);
        }
        else
        {
            selectImage.sprite = selectedSprite;
            ShotsPicker.instance.currentlySelectedIngredient.Add(thisIngredient);
        }
    }
}
