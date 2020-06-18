using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class DrinkPicker : MonoBehaviour
{
    public static DrinkPicker instance;

    [SerializeField] List<Drink> drinks;
    [SerializeField] GameObject content;
    [SerializeField] Text header;
    [SerializeField] GameObject alkoholBar, ingredientBar, drinkBar;
    [SerializeField] GameObject alkoholNextButton, ingredientNextButton;
    [SerializeField] GameObject backToAlkohols, backToIngredients, backToDrinks;

    List<Drink.Alkohol> alkohols = new List<Drink.Alkohol>();
    public List<Drink.Alkohol> currentlySelectedAlkohol = new List<Drink.Alkohol>();
    public List<Drink.Ingredient> currentlySelectedIngredient = new List<Drink.Ingredient>();
    public List<Drink> haveEverythingDrinks = new List<Drink>();
    public List<Drink> missingOnePartDrinks = new List<Drink>();
    public List<Drink> missingTwoPartDrinks = new List<Drink>();

    RecipeDisplay recipeDisplay;
    BackToStartButton backToStartButton;

    bool showingMissingOne, showingMissingTwo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (Drink drink in drinks)
        {
            foreach (Drink.Alkohol alkohol in drink.alkohols)
            {
                if (!alkohols.Contains(alkohol))
                {
                    alkohols.Add(alkohol);
                }
            }
        }

        recipeDisplay = FindObjectOfType<RecipeDisplay>();
        backToStartButton = FindObjectOfType<BackToStartButton>();

        CreateAlkoholsList();
        LoadIngredients();
    }

    public void CreateAlkoholsList()
    {
        ClearTheList();

        header.text = "Pick drinks that you have";

        CreatAlkoholList(alkohols);

        alkoholNextButton.SetActive(true);
        ingredientNextButton.SetActive(false);

        backToAlkohols.SetActive(false);
        backToDrinks.SetActive(false);
        backToIngredients.SetActive(false);
        backToStartButton.DisplayBackButton(true);
    }

    void CreatAlkoholList(List<Drink.Alkohol> items)
    {
        items.Sort();
        foreach (Drink.Alkohol item in items)
        {
            GameObject bar = Instantiate(alkoholBar, content.transform);
            bar.GetComponent<AlkoholBar>().PrepareBar(item);
        }
    }

    public void CreateIngredientsList()
    {
        ClearTheList();

        header.text = "Pick Ingredients that you have";

        List<Drink.Ingredient> items = new List<Drink.Ingredient>();
        foreach (Drink drink in drinks)
        {
            foreach (Drink.Ingredient ingredient in drink.ingredients)
            {
                if (!items.Contains(ingredient))
                {
                    items.Add(ingredient);
                }
            }
        }

        CreateIngredientList(items);

        alkoholNextButton.SetActive(false);
        ingredientNextButton.SetActive(true);
        backToAlkohols.SetActive(true);
        backToDrinks.SetActive(false);
        backToIngredients.SetActive(false);
        backToStartButton.DisplayBackButton(false);
    }

    void CreateIngredientList(List<Drink.Ingredient> items)
    {
        items.Sort();
        foreach (Drink.Ingredient item in items)
        {
            GameObject bar = Instantiate(ingredientBar, content.transform);
            bar.GetComponent<IngredientBar>().PrepareBar(item);
        }
    }

    public void CreateDrinksList()
    {
        ClearTheList();

        header.text = "Choose what drink to make";

        haveEverythingDrinks = new List<Drink>();
        missingOnePartDrinks = new List<Drink>();
        missingTwoPartDrinks = new List<Drink>();

        foreach (Drink drink in drinks)
        {
            int missingParts = 0;
            foreach (Drink.Alkohol alkohol in drink.alkohols)
            {
                if (!currentlySelectedAlkohol.Contains(alkohol))
                {
                    missingParts++;
                }
            }
            foreach (Drink.Ingredient ingredient in drink.ingredients)
            {
                if (!currentlySelectedIngredient.Contains(ingredient))
                {
                    missingParts++;
                }
            }

            switch (missingParts)
            {
                case (0):
                    haveEverythingDrinks.Add(drink);
                    break;
                case (1):
                    missingOnePartDrinks.Add(drink);
                    break;
                case (2):
                    missingTwoPartDrinks.Add(drink);
                    break;
            }

        }

        CreateDrinkList();

        alkoholNextButton.SetActive(false);
        ingredientNextButton.SetActive(false);
        backToAlkohols.SetActive(false);
        backToDrinks.SetActive(false);
        backToIngredients.SetActive(true);
        backToStartButton.DisplayBackButton(false);
    }

    void CreateDrinkList()
    {
        if (haveEverythingDrinks.Count > 0)
        {
            haveEverythingDrinks.Sort();
        }
        if (missingOnePartDrinks.Count > 0)
        {
            missingOnePartDrinks.Sort();
        }
        if (missingTwoPartDrinks.Count > 0)
        {
            missingTwoPartDrinks.Sort();
        }

        int itemCount = haveEverythingDrinks.Count;
        if (showingMissingOne)
        {
            itemCount += missingOnePartDrinks.Count;
        }
        if (showingMissingTwo)
        {
            itemCount += missingTwoPartDrinks.Count;
        }

        foreach (DrinkBar obj in content.GetComponentsInChildren<DrinkBar>())
        {
            Destroy(obj.gameObject);
        }
        foreach (Drink item in haveEverythingDrinks)
        {
            GameObject bar = Instantiate(drinkBar, content.transform);
            bar.GetComponent<DrinkBar>().PrepareBar(item);
        }
        //if (showingMissingOne)
        //{
        //    foreach (Drink item in missingOnePartDrinks)
        //    {
        //        GameObject bar = Instantiate(drinkBar, content.transform);
        //        //bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
        //        //currentStep -= stepSize;
        //        bar.GetComponent<DrinkBar>().PrepareBar(item, 1);
        //    }
        //}
        //if (showingMissingTwo)
        //{
        //    foreach (Drink item in missingTwoPartDrinks)
        //    {
        //        GameObject bar = Instantiate(drinkBar, content.transform);
        //        //bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
        //        //currentStep -= stepSize;
        //        bar.GetComponent<DrinkBar>().PrepareBar(item, 2);
        //    }
        //}
    }

    void ClearTheList()
    {
        foreach (Transform child in content.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject != content)
            {
                Destroy(child.gameObject);
            }
        }
        recipeDisplay.TurnOffDisplay();
    }

    public void ShowRecipe(Drink drink)
    {
        header.text = drink.name;
        backToAlkohols.SetActive(false);
        backToDrinks.SetActive(true);
        backToIngredients.SetActive(false);
        recipeDisplay.DisplayRecipe(drink.recipe);
        backToStartButton.DisplayBackButton(false);
    }

    public void CreateMissingOnePartList()
    {
        showingMissingOne = !showingMissingOne;

        CreateDrinkList();
    }

    public void CreateMissingTwoPartList()
    {
        showingMissingOne = true;
        showingMissingTwo = !showingMissingTwo;

        CreateDrinkList();
    }

    public void SaveIngredients()
    {
        PlayerPrefs.SetInt("IngredientsAmount", currentlySelectedIngredient.Count);
        for (int i = 0; i < currentlySelectedIngredient.Count; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), (int)currentlySelectedIngredient[i]);
        }
    }

    public void LoadIngredients()
    {
        if (PlayerPrefs.GetInt("IngredientsAmount") != 0)
        {
            currentlySelectedIngredient = new List<Drink.Ingredient>();
            for (int i = 0; i < PlayerPrefs.GetInt("IngredientsAmount"); i++)
            {
                currentlySelectedIngredient.Add((Drink.Ingredient)PlayerPrefs.GetInt(i.ToString()));
            }
        }
    }
}
