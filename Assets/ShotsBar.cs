using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsBar : MonoBehaviour
{
    [SerializeField] Text ingredientName;

    Shot thisShot;

    public void PrepareBar(Shot shot)
    {
        thisShot = shot;
        ingredientName.text = shot.name.ToString();
    }

    public void Pressed()
    {
        ShotsPicker.instance.ShowRecipe(thisShot);
    }
}
