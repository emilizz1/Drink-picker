using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkBar : MonoBehaviour
{
    [SerializeField] Text ingredientName;

    Drink thisDrink;

    public void PrepareBar(Drink drink, int missing = -1)
    {
        thisDrink = drink;
        if (missing != -1)
        {
            ingredientName.text = drink.name.ToString() + " (" + missing + ")";
        }
        else
        {
            ingredientName.text = drink.name.ToString();
        }
    }

    public void Pressed()
    {
        DrinkPicker.instance.ShowRecipe(thisDrink);
    }
}
