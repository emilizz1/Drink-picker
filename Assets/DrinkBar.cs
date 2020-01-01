using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkBar : MonoBehaviour
{
    [SerializeField] Text ingredientName;
    [SerializeField] Sprite selectedSprite, unselectedSprite;
    [SerializeField] Image selectImage;

    Drink thisDrink;

    public void PrepareBar(Drink drink)
    {
        thisDrink = drink;
        ingredientName.text = drink.name.ToString();
    }

    public void Pressed()
    {
        DrinkPicker.instance.ShowRecipe(thisDrink);
    }
}
