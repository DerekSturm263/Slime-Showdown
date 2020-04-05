using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseShop : MonoBehaviour
{
    private GameObject shopCanvas;
    private GameObject shopTopBarLayout;
    private GameObject shopMealsContent;
    private GameObject shopSnacksContent;
    private GameObject shopBackground;
    private GameObject shopBackButton;
    private GameObject shopSelector;

    private GameObject playerSlime;

    public GameObject firstSelectedItem;

    private void Start()
    {
        shopCanvas = GameObject.FindGameObjectWithTag("RanchShopCanvas");
        shopTopBarLayout = GameObject.FindGameObjectWithTag("RanchShopTopBar");
        shopMealsContent = GameObject.FindGameObjectWithTag("ShopMealsContent");
        shopSnacksContent = GameObject.FindGameObjectWithTag("ShopSnacksContent");
        shopBackground = GameObject.FindGameObjectWithTag("ShopBackground");
        shopBackButton = GameObject.FindGameObjectWithTag("ShopBackButton");
        shopSelector = GameObject.FindGameObjectWithTag("RanchShopSelector");

        playerSlime = GameObject.FindGameObjectWithTag("RanchBattleSlime");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OpenShop();
    }

    private void OpenShop()
    {
        GetComponent<ShopManager>().enabled = true;
        shopSelector.GetComponent<MoveToSelectedItem>().enabled = true;
        GetComponent<ShopManager>().selectedItem = firstSelectedItem;

        playerSlime.GetComponent<SlimeMove>().enabled = false;
        playerSlime.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        shopTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatIn");
        shopMealsContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        shopBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
        shopBackButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatIn");
    }

    // Method called by the back button in the shop or the escape button in the shop.
    public void CloseShop()
    {
        GetComponent<ShopManager>().enabled = false;
        shopSelector.GetComponent<MoveToSelectedItem>().enabled = false;

        playerSlime.GetComponent<SlimeMove>().enabled = true;

        shopTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatOut");
        shopMealsContent.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        shopBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
        shopBackButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");
    }
}
