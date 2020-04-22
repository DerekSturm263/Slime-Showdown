using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseButtonMethods : MonoBehaviour
{
    private OpenCloseStatsPage statsManager;
    private ShopManager shopManager;

    private GameObject uiBackground;
    private GameObject pauseButtons;
    private GameObject resumeButton;
    private GameObject backButtonOptions;
    private GameObject optionsElements;
    private GameObject options;

    private Toggle fullscreenToggle;
    private GameObject playerSlime;

    public bool isPauseOpen = false;
    private bool isOptionsOpen = false;

    private void Start()
    {
        statsManager = GameObject.FindGameObjectWithTag("Stats").GetComponent<OpenCloseStatsPage>();
        shopManager = GameObject.FindGameObjectWithTag("RanchShopkeeperSlime").GetComponent<ShopManager>();

        uiBackground = GameObject.FindGameObjectWithTag("ShopBackground");
        pauseButtons = GameObject.FindGameObjectWithTag("PauseButtons");
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        backButtonOptions = GameObject.FindGameObjectWithTag("OptionsBackButton");
        optionsElements = GameObject.FindGameObjectWithTag("Options");
        options = GameObject.FindGameObjectWithTag("AllOptions");

        fullscreenToggle = GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>();
        playerSlime = GameObject.FindGameObjectWithTag("RanchBattleSlime");
    }

    private void Update()
    {
        if (isPauseOpen)
        {
            if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause"))
            {
                if (!isOptionsOpen)
                    CloseSettings();
                else
                    CloseOptions();
            }
        }
        else if (!shopManager.isShopOpen && !statsManager.isStatsPageOpen)
        {
            if (Input.GetButtonDown("Pause"))
                OpenSettings();
        }
    }
    
    private void OpenSettings()
    {
        isPauseOpen = true;

        playerSlime.GetComponent<SlimeMove>().enabled = false;
        playerSlime.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        foreach (GameObject enemy in SpawnEnemies.enemies)
        {
            enemy.GetComponent<EnemySlimeMove>().enabled = false;
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }

        pauseButtons.GetComponent<CanvasGroup>().interactable = true;
        EventSystem.current.SetSelectedGameObject(resumeButton);

        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatIn");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");
    }

    private void CloseSettings()
    {
        isPauseOpen = false;

        playerSlime.GetComponent<SlimeMove>().enabled = true;

        foreach (GameObject enemy in SpawnEnemies.enemies)
        {
            enemy.GetComponent<EnemySlimeMove>().enabled = true;
        }

        pauseButtons.GetComponent<CanvasGroup>().interactable = false;

        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        uiBackground.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
    }

    public void ResumeButton()
    {
        CloseSettings();
    }

    public void OpenOptions()
    {
        isOptionsOpen = true;
        EventSystem.current.SetSelectedGameObject(fullscreenToggle.gameObject);

        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        backButtonOptions.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatIn");
        optionsElements.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeIn");

        options.GetComponent<CanvasGroup>().interactable = true;
        pauseButtons.GetComponent<CanvasGroup>().interactable = false;
    }

    public void CloseOptions()
    {
        isOptionsOpen = false;

        pauseButtons.GetComponent<Animation>().Play("ui_title_buttonLayout_floatIn");
        backButtonOptions.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");
        optionsElements.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        options.GetComponent<CanvasGroup>().interactable = true;
        pauseButtons.GetComponent<CanvasGroup>().interactable = true;
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

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().lastPlayerPos = playerSlime.transform.position;

        Invoke("LoadTitle", 0.5f);
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
