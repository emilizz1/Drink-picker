using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToStartButton : MonoBehaviour
{
    [SerializeField] GameObject backToStartButton;

    public void DisplayBackButton(bool active)
    {
        backToStartButton.SetActive(active);
    }
}
