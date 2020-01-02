using System.Collections;
using System.Collections.Generic;
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

    RecipeDisplay recipeDisplay;
    BackToStartButton backToStartButton;

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
        float currentStep = 0f, stepSize = alkoholBar.GetComponent<RectTransform>().sizeDelta.y + 25f;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, stepSize * items.Count - 25f);
        currentStep = (content.GetComponent<RectTransform>().sizeDelta.y / 2f) - (alkoholBar.GetComponent<RectTransform>().sizeDelta.y / 2f);
        foreach (Drink.Alkohol item in items)
        {
            GameObject bar = Instantiate(alkoholBar, content.transform);
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
            currentStep -= stepSize;
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
            bool selectedAllAlkohols = true;
            foreach (Drink.Alkohol alkohol in drink.alkohols)
            {
                if (!currentlySelectedAlkohol.Contains(alkohol))
                {
                    selectedAllAlkohols = false;
                }
            }
            if (selectedAllAlkohols)
            {
                foreach (Drink.Ingredient ingredient in drink.ingredients)
                {
                    if (!items.Contains(ingredient))
                    {
                        items.Add(ingredient);
                    }
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
        float currentStep = 0f, stepSize = ingredientBar.GetComponent<RectTransform>().sizeDelta.y + 25f;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, stepSize * items.Count - 25f);
        currentStep = (content.GetComponent<RectTransform>().sizeDelta.y / 2f) - (ingredientBar.GetComponent<RectTransform>().sizeDelta.y / 2f);
        foreach (Drink.Ingredient item in items)
        {
            GameObject bar = Instantiate(ingredientBar, content.transform);
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
            currentStep -= stepSize;
            bar.GetComponent<IngredientBar>().PrepareBar(item);
        }
    }

    public void CreateDrinksList()
    {
        ClearTheList();

        header.text = "Choose what drink to make";

        List<Drink> items = new List<Drink>();
        foreach (Drink drink in drinks)
        {
            bool selectedAllAlkohols = true;
            foreach (Drink.Alkohol alkohol in drink.alkohols)
            {
                if (!currentlySelectedAlkohol.Contains(alkohol))
                {
                    selectedAllAlkohols = false;
                }
            }
            if (selectedAllAlkohols)
            {
                bool allIngredientsPicked = true;
                foreach (Drink.Ingredient ingredient in drink.ingredients)
                {
                    if (!currentlySelectedIngredient.Contains(ingredient))
                    {
                        allIngredientsPicked = false;
                    }
                }
                if (allIngredientsPicked)
                {
                    items.Add(drink);
                }
            }
        }

        CreateDrinkList(items);

        alkoholNextButton.SetActive(false);
        ingredientNextButton.SetActive(false);
        backToAlkohols.SetActive(false);
        backToDrinks.SetActive(false);
        backToIngredients.SetActive(true);
        backToStartButton.DisplayBackButton(false);
    }

    void CreateDrinkList(List<Drink> items)
    {
        if (items.Count > 0)
        {
            items.Sort();
        }
        float stepSize = drinkBar.GetComponent<RectTransform>().sizeDelta.y + 25f;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, stepSize * items.Count - 25f);
        float currentStep = (content.GetComponent<RectTransform>().sizeDelta.y / 2f) - (drinkBar.GetComponent<RectTransform>().sizeDelta.y / 2f);
        foreach (Drink item in items)
        {
            GameObject bar = Instantiate(drinkBar, content.transform);
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
            currentStep -= stepSize;
            bar.GetComponent<DrinkBar>().PrepareBar(item);
        }
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
}
