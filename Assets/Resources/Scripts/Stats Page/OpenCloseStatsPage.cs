using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenCloseStatsPage : MonoBehaviour
{
    private enum Tabs
    {
        Stats, Items
    }
    private Tabs openTab = Tabs.Stats;

    private GameManager gameManager;
    private ShopManager shopManager;
    private PauseButtonMethods pauseManager;

    private GameObject statsPage;
    private GameObject inventoryPage;
    private GameObject statsPageTopBarLayout;
    private GameObject tabSwitcherBackground;
    private GameObject statsButton;
    private GameObject inventoryButton;

    private GameObject slimeIcon;
    private GameObject statsLayout;
    private GameObject uiBackground;

    private Slider waterBar;
    private Slider airBar;
    private Slider fireBar;
    private Slider earthBar;
    private Slider electricBar;

    private GameObject playerSlime;

    private bool usingAxisX = false;

    public bool isStatsPageOpen = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();
        pauseManager = EventSystem.current.GetComponent<PauseButtonMethods>();

        statsPage = GameObject.FindGameObjectWithTag("StatsPage");
        inventoryPage = GameObject.FindGameObjectWithTag("InventoryPage");
        statsPageTopBarLayout = GameObject.FindGameObjectWithTag("StatsPageTopBarLayout");
        tabSwitcherBackground = GameObject.FindGameObjectWithTag("TabSwitcherBackgroundStats");
        statsButton = GameObject.FindGameObjectWithTag("StatsButton");
        inventoryButton = GameObject.FindGameObjectWithTag("InventoryButton");

        slimeIcon = GameObject.FindGameObjectWithTag("SlimeIcon");
        statsLayout = GameObject.FindGameObjectWithTag("StatsLayout");
        uiBackground = GameObject.FindGameObjectWithTag("ShopBackground");

        waterBar = GameObject.FindGameObjectWithTag("WaterBar").GetComponent<Slider>();
        airBar = GameObject.FindGameObjectWithTag("AirBar").GetComponent<Slider>();
        fireBar = GameObject.FindGameObjectWithTag("FireBar").GetComponent<Slider>();
        earthBar = GameObject.FindGameObjectWithTag("EarthBar").GetComponent<Slider>();
        electricBar = GameObject.FindGameObjectWithTag("ElectricBar").GetComponent<Slider>();

        playerSlime = GameObject.FindGameObjectWithTag("RanchBattleSlime");
    }

    private void Update()
    {
        if (Input.GetButtonDown("StatsPage") && !shopManager.isShopOpen && !pauseManager.isPauseOpen)
        {
            if (!isStatsPageOpen)
                OpenStatsPage();
            else
                CloseStatsPage();
        }

        if (Input.GetButtonDown("Cancel") && isStatsPageOpen)
        {
            CloseStatsPage();
        }

        // Sets the bars to reflect the player's current stats.
        waterBar.value = gameManager.playSeafoodAff;
        airBar.value = gameManager.playCandyAff;
        fireBar.value = gameManager.playSpicyAff;
        earthBar.value = gameManager.playVeggieAff;
        electricBar.value = gameManager.playSourAff;



        if (isStatsPageOpen)
        {
            if (!usingAxisX)
            {
                if ((Input.GetAxisRaw("Horizontal") > 0 || Input.GetButtonDown("R")) && openTab == Tabs.Stats && statsPage.GetComponent<CanvasGroup>().alpha == 1f)
                {
                    usingAxisX = true;
                    SwitchToItemsPage();
                }
                else if ((Input.GetAxisRaw("Horizontal") < 0 || Input.GetButtonDown("L")) && openTab == Tabs.Items && inventoryPage.GetComponent<CanvasGroup>().alpha == 1f)
                {
                    usingAxisX = true;
                    SwitchToStatsPage();
                }
            }

            #region Axis Resetting and Buttons

            if (Input.GetAxisRaw("Horizontal") == 0)
                usingAxisX = false;

            #endregion
        }
    }

    public void SwitchToStatsPage()
    {
        if (openTab != Tabs.Stats)
        {
            openTab = Tabs.Stats;

            statsPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
            inventoryPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
            statsButton.GetComponent<Animation>().Play("ui_ranch_shopSnacksButtonGrow");
            inventoryButton.GetComponent<Animation>().Play("ui_ranch_shopSnacksButtonShrink");
            tabSwitcherBackground.GetComponent<Animation>().Play("ui_ranch_shopTopBar_spinMeals");
        }
    }

    public void SwitchToItemsPage()
    {
        if (openTab != Tabs.Items)
        {
            openTab = Tabs.Items;

            statsPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
            inventoryPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
            statsButton.GetComponent<Animation>().Play("ui_ranch_shopSnacksButtonShrink");
            inventoryButton.GetComponent<Animation>().Play("ui_ranch_shopSnacksButtonGrow");
            tabSwitcherBackground.GetComponent<Animation>().Play("ui_ranch_shopTopBar_spinSnacks");
        }
    }

    private void OpenStatsPage()
    {
        SwitchToStatsPage();
        isStatsPageOpen = true;

        playerSlime.GetComponent<SlimeMove>().enabled = false;
        playerSlime.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        statsPageTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatIn");
        statsPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");

        slimeIcon.GetComponent<Animation>().Play("ui_ranchStats_slimeIcon_floatIn");
        statsLayout.GetComponent<Animation>().Play("ui_ranchStats_statsLayout_floatIn");
    }

    private void CloseStatsPage()
    {
        isStatsPageOpen = false;

        playerSlime.GetComponent<SlimeMove>().enabled = true;

        statsPageTopBarLayout.GetComponent<Animation>().Play("ui_ranch_shopTopBar_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        slimeIcon.GetComponent<Animation>().Play("ui_ranchStats_slimeIcon_floatOut");
        statsLayout.GetComponent<Animation>().Play("ui_ranchStats_statsLayout_floatOut");

        if (openTab == Tabs.Stats)
            statsPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
        else
            inventoryPage.GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
    }
}
