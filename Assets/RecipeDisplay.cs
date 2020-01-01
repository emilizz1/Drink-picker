using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDisplay : MonoBehaviour
{
    Text text;
    Image image;
    GameObject drinkPicker;

    void Start()
    {

        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();

        drinkPicker = FindObjectOfType<DrinkPicker>().gameObject;

        TurnOffDisplay();
    }

    public void DisplayRecipe(string recipe)
    {
        text.enabled = true;
        image.enabled = true;

        text.text = recipe;
        drinkPicker.SetActive(false);
    }

    public void TurnOffDisplay()
    {
        if(text == null)
        {
            text = GetComponentInChildren<Text>();
            image = GetComponent<Image>();

            drinkPicker = FindObjectOfType<DrinkPicker>().gameObject;
        }
        text.enabled = false;
        image.enabled = false;

        drinkPicker.SetActive(true);
    }
}
