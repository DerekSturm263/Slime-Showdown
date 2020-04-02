using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonMethods : MonoBehaviour
{
    private Animation gameLogoAnim;
    private Animation buttonLayoutAnim;

    private void Start()
    {
        gameLogoAnim = GameObject.FindGameObjectWithTag("TitleGameLogo").GetComponent<Animation>();
        buttonLayoutAnim = GameObject.FindGameObjectWithTag("TitleButtonLayout").GetComponent<Animation>();
    }

    public void StartGame()
    {
        gameLogoAnim.Play("ui_title_gameLogo_floatOut");
        buttonLayoutAnim.Play("ui_title_buttonLayout_floatOut");

        Invoke("LoadSlimePickScene", 1f);
    }

    public void LoadCredits()
    {
        gameLogoAnim.Play("ui_title_gameLogo_floatOut");
        buttonLayoutAnim.Play("ui_title_buttonLayout_floatOut");

        Invoke("LoadCreditsScene", 1f);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadSlimePickScene()
    {
        SceneManager.LoadScene("ChooseSlime");
    }

    private void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
