using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotsPicker : MonoBehaviour
{
    public static ShotsPicker instance;

    [SerializeField] List<Shot> shots;
    [SerializeField] GameObject content;
    [SerializeField] Text header;
    [SerializeField] GameObject shotIngredientsBar, shotsBar;
    [SerializeField] GameObject ingredientNextButton;
    [SerializeField] GameObject backToIngredients, backToShots;

    public List<Shot.Ingredient> currentlySelectedIngredient = new List<Shot.Ingredient>();

    ShotsRecipeDisplay recipeDisplay;
    BackToStartButton backToStartButton;

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        recipeDisplay = FindObjectOfType<ShotsRecipeDisplay>();
        backToStartButton = FindObjectOfType<BackToStartButton>();

        CreateIngredientsList();
    }

    public void CreateIngredientsList()
    {
        ClearTheList();

        header.text = "Pick Ingredients that you have";

        List<Shot.Ingredient> items = new List<Shot.Ingredient>();
        foreach (Shot drink in shots)
        {
            foreach (Shot.Ingredient ingredient in drink.ingredients)
            {
                if (!items.Contains(ingredient))
                {
                    items.Add(ingredient);
                }
            }
        }

        CreateIngredientList(items);

        ingredientNextButton.SetActive(true);
        backToShots.SetActive(false);
        backToIngredients.SetActive(false);
        backToStartButton.DisplayBackButton(true);
    }

    void CreateIngredientList(List<Shot.Ingredient> items)
    {
        items.Sort();
        float currentStep = 0f, stepSize = shotIngredientsBar.GetComponent<RectTransform>().sizeDelta.y + 25f;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, stepSize * items.Count - 25f);
        currentStep = (content.GetComponent<RectTransform>().sizeDelta.y / 2f) - (shotIngredientsBar.GetComponent<RectTransform>().sizeDelta.y / 2f);
        foreach (Shot.Ingredient item in items)
        {
            GameObject bar = Instantiate(shotIngredientsBar, content.transform);
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
            currentStep -= stepSize;
            bar.GetComponent<ShotsIngredientBar>().PrepareBar(item);
        }
    }

    public void CreateDrinksList()
    {
        ClearTheList();

        header.text = "Choose what drink to make";

        List<Shot> items = new List<Shot>();
        foreach (Shot drink in shots)
        {
            bool allIngredientsPicked = true;
            foreach (Shot.Ingredient ingredient in drink.ingredients)
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

        CreateDrinkList(items);

        ingredientNextButton.SetActive(false);
        backToShots.SetActive(false);
        backToIngredients.SetActive(true);
        backToStartButton.DisplayBackButton(false);
    }

    void CreateDrinkList(List<Shot> items)
    {
        if (items.Count > 0)
        {
            items.Sort();
        }
        float stepSize = shotsBar.GetComponent<RectTransform>().sizeDelta.y + 25f;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, stepSize * items.Count - 25f);
        float currentStep = (content.GetComponent<RectTransform>().sizeDelta.y / 2f) - (shotsBar.GetComponent<RectTransform>().sizeDelta.y / 2f);
        foreach (Shot item in items)
        {
            GameObject bar = Instantiate(shotsBar, content.transform);
            bar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, currentStep);
            currentStep -= stepSize;
            bar.GetComponent<ShotsBar>().PrepareBar(item);
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

    public void ShowRecipe(Shot drink)
    {
        header.text = drink.name;
        backToShots.SetActive(true);
        backToIngredients.SetActive(false);
        recipeDisplay.DisplayRecipe(drink.recipe);
        backToStartButton.DisplayBackButton(false);
    }
}
