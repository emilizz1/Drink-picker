using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AlkoholBar : MonoBehaviour
{
    [SerializeField] Text alkoholName;
    [SerializeField] Sprite selectedSprite, unselectedSprite;
    [SerializeField] Image selectImage;

    Drink.Alkohol thisAlkohol;

    private void Start()
    {
        if (DrinkPicker.instance.currentlySelectedAlkohol.Contains(thisAlkohol))
        {
            selectImage.sprite = selectedSprite;
        }
    }

    public void PrepareBar(Drink.Alkohol alkohol)
    {
        thisAlkohol = alkohol;
        alkoholName.text = alkohol.ToString();
    }

    public void Pressed()
    {
        if(selectImage.sprite == selectedSprite)
        {
            selectImage.sprite = unselectedSprite;
            DrinkPicker.instance.currentlySelectedAlkohol.Remove(thisAlkohol);
        }
        else
        {
            selectImage.sprite = selectedSprite;
            DrinkPicker.instance.currentlySelectedAlkohol.Add(thisAlkohol);
        }
    }
}
