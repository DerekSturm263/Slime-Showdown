using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseButtonMethods : MonoBehaviour
{
    private OpenCloseStatsPage statsManager;
    private ShopManager shopManager;

    private GameObject uiBackground;
    private GameObject pauseButtons;
    private GameObject resumeButton;

    private GameObject playerSlime;

    public bool isPauseOpen = false;

    private void Start()
    {
        statsManager = GameObject.FindGameObjectWithTag("Stats").GetComponent<OpenCloseStatsPage>();
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();

        uiBackground = GameObject.FindGameObjectWithTag("ShopBackground");
        pauseButtons = GameObject.FindGameObjectWithTag("PauseButtons");
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");

        playerSlime = GameObject.FindGameObjectWithTag("RanchBattleSlime");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && !shopManager.isShopOpen && !statsManager.isStatsPageOpen)
        {
            isPauseOpen = !isPauseOpen;

            if (isPauseOpen)
                OpenSettings();
            else
                CloseSettings();
        }
    }
    
    private void OpenSettings()
    {
        playerSlime.GetComponent<SlimeMove>().enabled = false;
        pauseButtons.GetComponent<CanvasGroup>().interactable = true;
        EventSystem.current.SetSelectedGameObject(resumeButton);

        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatIn");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
    }

    private void CloseSettings()
    {
        playerSlime.GetComponent<SlimeMove>().enabled = true;
        pauseButtons.GetComponent<CanvasGroup>().interactable = false;

        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
    }

    public void ResumeButton()
    {
        CloseSettings();
    }

    public void OptionsButton()
    {
        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        pauseButtons.GetComponent<CanvasGroup>().interactable = false;

        Invoke("LoadOptionsScene", 0.5f);
    }

    public void CreditsButton()
    {
        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        pauseButtons.GetComponent<CanvasGroup>().interactable = false;

        Camera.main.GetComponent<CameraFollow>().enabled = false;
        Camera.main.GetComponent<FocusOnSlime>().enabled = true;

        Invoke("LoadCreditsScene", 0.5f);
    }

    public void TitleButton()
    {
        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        pauseButtons.GetComponent<CanvasGroup>().interactable = false;

        Camera.main.GetComponent<CameraFollow>().enabled = false;
        Camera.main.GetComponent<MoveLerp>().enabled = true;

        Invoke("LoadTitle", 0.5f);
    }

    private void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

    private void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    private void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
