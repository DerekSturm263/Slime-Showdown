using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    private GameObject gameManager;

    private GameObject food;
    private GameObject foodTemplate;
    private GameObject shopMealsContent;
    private GameObject shopSnacksContent;

    public GameObject selectedItem;

    private bool usingAxisY = false;
    public bool isShopOpen = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        food = GameObject.FindGameObjectWithTag("Food");
        foodTemplate = GameObject.FindGameObjectWithTag("FoodTemplate");
        shopMealsContent = GameObject.FindGameObjectWithTag("ShopMealsContent");
        shopSnacksContent = GameObject.FindGameObjectWithTag("ShopSnacksContent");

        GenerateItems();
    }

    private void Update()
    {
        if (isShopOpen)
        {
            if (!usingAxisY)
            {
                if (Input.GetAxisRaw("Vertical") > 0 && selectedItem.GetComponent<Buyable>().itemUp != null)
                {
                    usingAxisY = true;
                    selectedItem = selectedItem.GetComponent<Buyable>().itemUp;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && selectedItem.GetComponent<Buyable>().itemDown != null)
                {
                    usingAxisY = true;
                    selectedItem = selectedItem.GetComponent<Buyable>().itemDown;
                }
            }

            if (Input.GetButtonDown("Submit"))
            {
                if (gameManager.goldCount >= selectedItem.GetComponent<Buybable>().price)
                {
                    gameManager.goldCount -= selectedItem.GetComponent<Buybable>().price;
                    Debug.Log("You bought a " + selectedItem.GetComponent<Buybable>().name);

                    if (selectedItem.GetComponent<Buyable>() is MealData)
                    {
                        // Automatically activate the meal.
                    }
                    else
                    {
                        foreach (SnackItem item in gameManager.inventory)
                        {
                            if (Array[item] == null)
                            {
                                gameManager.inventory[Array.IndexOf(gameManager.inventory, item)] = selectedItem.GetComponent<SnackItem>();

                                break;
                            }
                        }
                    }
                }
            }

            if (Input.GetAxisRaw("Vertical") == 0)
                usingAxisY = false;
        }
    }

    private void GenerateItems()
    {
        var scripts = food.GetComponents<MonoBehaviour>();

        for (int i = 0; i < scripts.Length; i++)
        {
            GenerateInShop(scripts[i] as Buyable);
        }

        foodTemplate.SetActive(false);
        food.SetActive(false);
    }

    private void GenerateInShop(Buyable item)
    {
        GameObject newItem = Instantiate(foodTemplate);

        newItem.name = item.foodName;

        if (item is MealData)
        {
            MealData mealScript = newItem.AddComponent<MealData>();

            mealScript.type = item.GetComponent<MealData>().type;
            mealScript.foodName = item.GetComponent<MealData>().foodName;
            mealScript.description = item.GetComponent<MealData>().description;
            mealScript.price = item.GetComponent<MealData>().price;
            mealScript.affinityIncrease = item.GetComponent<MealData>().affinityIncrease;

            newItem.transform.SetParent(shopMealsContent.transform);
        }
        else
        {
            SnackData snackScript = newItem.AddComponent<SnackData>();

            snackScript.type = item.GetComponent<SnackData>().type;
            snackScript.foodName = item.GetComponent<SnackData>().foodName;
            snackScript.description = item.GetComponent<SnackData>().description;
            snackScript.price = item.GetComponent<SnackData>().price;
            snackScript.hungerIncrease = item.GetComponent<SnackData>().price;

            newItem.transform.SetParent(shopSnacksContent.transform);
        }

        newItem.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
        newItem.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = item.price.ToString();
        newItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item.foodName;
        newItem.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = item.description;
    }
}
