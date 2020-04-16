using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenCloseStatsPage : MonoBehaviour
{
    private GameManager gameManager;
    private ShopManager shopManager;
    private PauseButtonMethods pauseManager;

    private GameObject slimeIcon;
    private GameObject statsLayout;
    private GameObject uiBackground;

    private Slider waterBar;
    private Slider airBar;
    private Slider fireBar;
    private Slider earthBar;
    private Slider electricBar;

    private GameObject playerSlime;

    public bool isStatsPageOpen = false;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();
        pauseManager = EventSystem.current.GetComponent<PauseButtonMethods>();

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

        waterBar.value = gameManager.playSeafoodAff;
        airBar.value = gameManager.playCandyAff;
        fireBar.value = gameManager.playSpicyAff;
        earthBar.value = gameManager.playVeggieAff;
        electricBar.value = gameManager.playSourAff;
    }

    private void OpenStatsPage()
    {
        isStatsPageOpen = true;
        playerSlime.GetComponent<SlimeMove>().enabled = false;

        slimeIcon.GetComponent<Animation>().Play("ui_ranchStats_slimeIcon_floatIn");
        statsLayout.GetComponent<Animation>().Play("ui_ranchStats_statsLayout_floatIn");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
    }

    private void CloseStatsPage()
    {
        isStatsPageOpen = false;
        playerSlime.GetComponent<SlimeMove>().enabled = true;

        slimeIcon.GetComponent<Animation>().Play("ui_ranchStats_slimeIcon_floatOut");
        statsLayout.GetComponent<Animation>().Play("ui_ranchStats_statsLayout_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
    }
}
