using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsRecipeDisplay : MonoBehaviour
{
    Text text;
    Image image;
    GameObject shotsPicker;

    void Start()
    {

        text = GetComponentInChildren<Text>();
        image = GetComponent<Image>();

        shotsPicker = FindObjectOfType<ShotsPicker>().gameObject;

        TurnOffDisplay();
    }

    public void DisplayRecipe(string recipe)
    {
        text.enabled = true;
        image.enabled = true;

        text.text = recipe;
        shotsPicker.SetActive(false);
    }

    public void TurnOffDisplay()
    {
        if (text == null)
        {
            text = GetComponentInChildren<Text>();
            image = GetComponent<Image>();

            shotsPicker = FindObjectOfType<ShotsPicker>().gameObject;
        }
        text.enabled = false;
        image.enabled = false;

        shotsPicker.SetActive(true);
    }
}
