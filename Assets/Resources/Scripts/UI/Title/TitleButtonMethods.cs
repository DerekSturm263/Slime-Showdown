﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonMethods : MonoBehaviour
{
    private GameObject gameManager;

    private GameObject gameLogo;
    private GameObject buttonLayout;
    private GameObject versionNumber;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        gameLogo = GameObject.FindGameObjectWithTag("TitleGameLogo");
        buttonLayout = GameObject.FindGameObjectWithTag("TitleButtonLayout");
        versionNumber = GameObject.FindGameObjectWithTag("TitleVersionNumber");
    }

    public void StartGame()
    {
        gameLogo.GetComponent<Animation>().Play("ui_title_gameLogo_floatOut");
        buttonLayout.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        versionNumber.GetComponent<Animation>().Play("ui_title_versionNumber_floatOut");

        buttonLayout.GetComponent<CanvasGroup>().interactable = false;

        Invoke("LoadSlimePickScene", 1f);
    }

    public void LoadOptions()
    {
        gameLogo.GetComponent<Animation>().Play("ui_title_gameLogo_floatOut");
        buttonLayout.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        versionNumber.GetComponent<Animation>().Play("ui_title_versionNumber_floatOut");

        buttonLayout.GetComponent<CanvasGroup>().interactable = false;

        Invoke("LoadOptionsScene", 1f);
    }

    public void LoadCredits()
    {
        gameLogo.GetComponent<Animation>().Play("ui_title_gameLogo_floatOut");
        buttonLayout.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        versionNumber.GetComponent<Animation>().Play("ui_title_versionNumber_floatOut");

        buttonLayout.GetComponent<CanvasGroup>().interactable = false;

        Invoke("LoadCreditsScene", 1f);
    }

    public void QuitGame()
    {
        gameLogo.GetComponent<Animation>().Play("ui_title_gameLogo_floatOut");
        buttonLayout.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        versionNumber.GetComponent<Animation>().Play("ui_title_versionNumber_floatOut");

        buttonLayout.GetComponent<CanvasGroup>().interactable = false;

        Invoke("Quit", 1f);
    }

    private void LoadSlimePickScene()
    {
        SceneManager.LoadScene("ChooseSlime");
        DontDestroyOnLoad(gameManager);
        DontDestroyOnLoad(Camera.main);
        Camera.main.GetComponent<MoveLerp>().enabled = false;
        Camera.main.GetComponent<FocusOnSlime>().enabled = true;
    }

    private void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");
        DontDestroyOnLoad(gameManager);
    }

    private void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
        DontDestroyOnLoad(Camera.main);
    }

    private void Quit()
    {
        Application.Quit();
    }
}