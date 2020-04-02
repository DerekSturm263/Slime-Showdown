using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonMethods : MonoBehaviour
{
    private GameObject gameManager;

    private GameObject gameLogo;
    private GameObject buttonLayout;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        gameLogo = GameObject.FindGameObjectWithTag("TitleGameLogo");
        buttonLayout = GameObject.FindGameObjectWithTag("TitleButtonLayout");
    }

    public void StartGame()
    {
        gameLogo.GetComponent<Animation>().Play("ui_title_gameLogo_floatOut");
        buttonLayout.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");
        buttonLayout.GetComponent<CanvasGroup>().interactable = false;

        Invoke("LoadSlimePickScene", 1f);
    }

    public void LoadCredits()
    {
        gameLogo.GetComponent<Animation>().Play("ui_title_gameLogo_floatOut");
        buttonLayout.GetComponent<Animation>().Play("ui_title_buttonLayout_floatOut");

        Invoke("LoadCreditsScene", 1f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadSlimePickScene()
    {
        SceneManager.LoadScene("ChooseSlime");
        DontDestroyOnLoad(gameManager);
    }

    private void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
