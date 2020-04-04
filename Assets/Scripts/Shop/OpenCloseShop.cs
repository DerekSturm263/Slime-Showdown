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

    private void Start()
    {
        shopCanvas = GameObject.FindGameObjectWithTag("RanchShopCanvas");
        shopTopBarLayout = GameObject.FindGameObjectWithTag("RanchShopTopBar");
        shopMealsContent = GameObject.FindGameObjectWithTag("ShopMealsContent");
        shopSnacksContent = GameObject.FindGameObjectWithTag("ShopSnacksContent");
        shopBackground = GameObject.FindGameObjectWithTag("ShopBackground");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        OpenShop();
    }

    private void OpenShop()
    {
        shopCanvas.GetComponent<CanvasGroup>().alpha = 1;
        shopCanvas.GetComponent<CanvasGroup>().interactable = true;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

        shopTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatIn");
        shopBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
    }

    public void CloseShop()
    {
        shopCanvas.GetComponent<CanvasGroup>().alpha = 0;
        shopCanvas.GetComponent<CanvasGroup>().interactable = false;
        shopCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        shopTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatOut");
        shopBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
    }
}
