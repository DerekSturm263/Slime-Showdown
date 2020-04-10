using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private enum Tabs
    {
        Meals, Snacks
    }
    private Tabs openTab = Tabs.Meals;

    private GameObject gameManager;

    private GameObject food;
    private GameObject foodTemplate;
    private GameObject shopScrollView;
    private GameObject shopMealsContent;
    private GameObject shopSnacksContent;
    private GameObject shopCanvas;
    private GameObject shopTopBarLayout;
    private GameObject shopBackground;
    private GameObject shopBackButton;
    private GameObject shopSelector;

    private Scrollbar shopScrollBar;
    private float scrollAmount;

    public List<GameObject> meals;
    public List<GameObject> snacks;

    private uint shopMealPos = 0;
    private uint shopSnackPos = 0;

    private GameObject playerSlime;

    public GameObject firstSelectedItem;
    public GameObject selectedItem;

    private bool usingAxisX = false;
    private bool usingAxisY = false;

    public bool isShopOpen = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        food = GameObject.FindGameObjectWithTag("Food");
        foodTemplate = GameObject.FindGameObjectWithTag("FoodTemplate");
        shopScrollView = GameObject.FindGameObjectWithTag("ShopScrollView");
        shopMealsContent = GameObject.FindGameObjectWithTag("ShopMealsContent");
        shopSnacksContent = GameObject.FindGameObjectWithTag("ShopSnacksContent");
        shopCanvas = GameObject.FindGameObjectWithTag("RanchShopCanvas");
        shopTopBarLayout = GameObject.FindGameObjectWithTag("RanchShopTopBar");
        shopBackground = GameObject.FindGameObjectWithTag("ShopBackground");
        shopBackButton = GameObject.FindGameObjectWithTag("ShopBackButton");
        shopSelector = GameObject.FindGameObjectWithTag("RanchShopSelector");
        shopScrollBar = GameObject.FindGameObjectWithTag("RanchShopScrollBar").GetComponent<Scrollbar>();

        playerSlime = GameObject.FindGameObjectWithTag("RanchBattleSlime");

        GenerateItems();
    }

    private void Update()
    {
        if (isShopOpen)
        {
            if (!usingAxisX)
            {
                if (Input.GetAxisRaw("Horizontal") > 0 && openTab == Tabs.Meals && shopMealsContent.GetComponent<CanvasGroup>().alpha == 1f)
                {
                    usingAxisX = true;
                    openTab = Tabs.Snacks;
                    shopScrollView.GetComponent<ScrollRect>().content = shopSnacksContent.GetComponent<RectTransform>();
                    shopScrollBar.value = 1f;

                    selectedItem = snacks[0];

                    shopMealsContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
                    shopSnacksContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
                }
                else if (Input.GetAxisRaw("Horizontal") < 0 && openTab == Tabs.Snacks && shopSnacksContent.GetComponent<CanvasGroup>().alpha == 1f)
                {
                    usingAxisX = true;
                    openTab = Tabs.Meals;
                    shopScrollView.GetComponent<ScrollRect>().content = shopMealsContent.GetComponent<RectTransform>();
                    shopScrollBar.value = 1f;

                    selectedItem = meals[0];

                    shopMealsContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
                    shopSnacksContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
                }
            }

            scrollAmount = ( openTab == Tabs.Meals ) ? 0.2f : 0.0718f;

            if (!usingAxisY)
            {
                if (Input.GetAxisRaw("Vertical") > 0 && selectedItem.GetComponent<Buyable>().itemUp != null)
                {
                    usingAxisY = true;
                    selectedItem = selectedItem.GetComponent<Buyable>().itemUp;
                    shopScrollBar.value += scrollAmount;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && selectedItem.GetComponent<Buyable>().itemDown != null)
                {
                    usingAxisY = true;
                    selectedItem = selectedItem.GetComponent<Buyable>().itemDown;
                    shopScrollBar.value -= scrollAmount;
                }
            }

            if (Input.GetButtonDown("Submit"))
                BuyItem();

            if (Input.GetAxisRaw("Horizontal") == 0)
                usingAxisX = false;

            if (Input.GetAxisRaw("Vertical") == 0)
                usingAxisY = false;

            if (Input.GetButtonDown("Cancel"))
                CloseShop();
        }
    }

    public void BuyItem()
    {
        if (gameManager.GetComponent<GameManager>().goldCount >= selectedItem.GetComponent<Buyable>().price)
        {
            if (selectedItem.GetComponent<Buyable>() is MealData)
            {
                gameManager.GetComponent<GameManager>().goldCount -= selectedItem.GetComponent<Buyable>().price;
                EatMeal(selectedItem.GetComponent<Buyable>());
            }
            else
            {
                if (gameManager.GetComponent<GameManager>().inventory[gameManager.GetComponent<GameManager>().inventory.Length - 1] == null)
                {
                    gameManager.GetComponent<GameManager>().goldCount -= selectedItem.GetComponent<Buyable>().price;

                    for (int i = 0; i < gameManager.GetComponent<GameManager>().inventory.Length; i++)
                    {
                        if (gameManager.GetComponent<GameManager>().inventory[i] == null)
                        {
                            gameManager.GetComponent<GameManager>().inventory[i] = selectedItem.GetComponent<SnackData>().inventoryVersion;

                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("You don't have enough money for that!");
        }
    }

    private void EatMeal(Buyable food)
    {

    }

    // Method called by the shopkeeper slime when the player touches it.
    public void OpenShop()
    {
        openTab = Tabs.Meals;
        isShopOpen = true;

        shopCanvas.GetComponent<CanvasGroup>().interactable = true;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

        GetComponent<ShopManager>().enabled = true;
        shopSelector.GetComponent<MoveToSelectedItem>().enabled = true;
        GetComponent<ShopManager>().selectedItem = firstSelectedItem;

        playerSlime.GetComponent<SlimeMove>().enabled = false;
        playerSlime.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        shopTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatIn");
        shopScrollView.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        shopMealsContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        shopBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
        shopBackButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatIn");
    }

    // Method called by the back button in the shop or the escape button in the shop.
    public void CloseShop()
    {
        isShopOpen = false;
        shopScrollBar.value = 1f;

        shopCanvas.GetComponent<CanvasGroup>().interactable = false;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        GetComponent<ShopManager>().enabled = false;
        shopSelector.GetComponent<MoveToSelectedItem>().enabled = false;

        playerSlime.GetComponent<SlimeMove>().enabled = true;

        shopTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatOut");
        shopScrollView.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        shopBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
        shopBackButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");

        if (openTab == Tabs.Meals)
        {
            shopMealsContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        }
        else
        {
            shopSnacksContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        }
    }

    private void GenerateItems()
    {
        var scripts = food.GetComponents<MonoBehaviour>();

        for (int i = 0; i < scripts.Length; i++)
        {
            GenerateInShop(scripts[i] as Buyable);
        }

        GenerateAllRelations();

        Destroy(foodTemplate);
        Destroy(food);
    }

    private void GenerateInShop(Buyable item)
    {
        GameObject newItem = Instantiate(foodTemplate);

        if (firstSelectedItem == null) firstSelectedItem = newItem;

        newItem.name = item.foodName;

        if (item is MealData)
        {
            MealData mealScript = newItem.AddComponent<MealData>();

            mealScript.type = item.GetComponent<MealData>().type;
            mealScript.foodName = item.GetComponent<MealData>().foodName;
            mealScript.description = item.GetComponent<MealData>().description;
            mealScript.price = item.GetComponent<MealData>().price;
            mealScript.affinityIncrease = item.GetComponent<MealData>().affinityIncrease;
            mealScript.mealShopPos = shopMealPos++;

            newItem.transform.SetParent(shopMealsContent.transform);

            meals.Add(newItem);
        }
        else
        {
            SnackData snackScript = newItem.AddComponent<SnackData>();

            snackScript.type = item.GetComponent<SnackData>().type;
            snackScript.foodName = item.GetComponent<SnackData>().foodName;
            snackScript.description = item.GetComponent<SnackData>().description;
            snackScript.price = item.GetComponent<SnackData>().price;
            snackScript.hungerIncrease = item.GetComponent<SnackData>().price;
            snackScript.inventoryVersion = item.GetComponent<SnackData>().inventoryVersion;
            snackScript.snackShopPos = shopSnackPos++;

            newItem.transform.SetParent(shopSnacksContent.transform);

            snacks.Add(newItem);
        }

        newItem.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
        newItem.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = "$" + item.price.ToString();
        newItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item.foodName;
        newItem.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = item.description;
    }

    private void GenerateAllRelations()
    {
        foreach (GameObject meal in meals)
        {
            if (meal.GetComponent<MealData>().mealShopPos > 0)
            {
                foreach (GameObject meal2 in meals)
                {
                    if (meal2.GetComponent<MealData>().mealShopPos == meal.GetComponent<MealData>().mealShopPos - 1)
                    {
                        meal.GetComponent<MealData>().itemUp = meal2;
                        break;
                    }
                }
            }

            if (meal.GetComponent<MealData>().mealShopPos < meals.Count - 1)
            {
                foreach (GameObject meal2 in meals)
                {
                    if (meal2.GetComponent<MealData>().mealShopPos == meal.GetComponent<MealData>().mealShopPos + 1)
                    {
                        meal.GetComponent<MealData>().itemDown = meal2;
                        break;
                    }
                }
            }
        }

        foreach (GameObject snack in snacks)
        {
            if (snack.GetComponent<SnackData>().snackShopPos > 0)
            {
                foreach (GameObject snack2 in snacks)
                {
                    if (snack2.GetComponent<SnackData>().snackShopPos == snack.GetComponent<SnackData>().snackShopPos - 1)
                    {
                        snack.GetComponent<SnackData>().itemUp = snack2;
                        break;
                    }
                }
            }

            if (snack.GetComponent<SnackData>().snackShopPos < snacks.Count - 1)
            {
                foreach (GameObject snack2 in snacks)
                {
                    if (snack2.GetComponent<SnackData>().snackShopPos == snack.GetComponent<SnackData>().snackShopPos + 1)
                    {
                        snack.GetComponent<SnackData>().itemDown = snack2;
                        break;
                    }
                }
            }
        }
    }
}
