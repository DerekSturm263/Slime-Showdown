using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMethods : MonoBehaviour
{
    private GameObject gameManager;

    private CanvasGroup confirmationUI;
    private CanvasGroup promptUI;
    private CanvasGroup namingUI;

    private Text slimeNameField;

    private void Start()
    {
        try
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController");

            confirmationUI = GameObject.FindGameObjectWithTag("ConfirmationUI").GetComponent<CanvasGroup>();
            promptUI = GameObject.FindGameObjectWithTag("PromptUI").GetComponent<CanvasGroup>();
            namingUI = GameObject.FindGameObjectWithTag("NamingUI").GetComponent<CanvasGroup>();

            slimeNameField = GameObject.FindGameObjectWithTag("SlimeNameField").GetComponent<Text>();
        }
        catch { }
    }

    #region Title Scene

    public void ToSlimePick()
    {
        SceneManager.LoadScene("PickSlime");
    }

    public void ToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region Slime Picking Scene

    // This is the method called by the "Yeah!" button when selecting your slime.
    public void PickSlime()
    {
        StartCoroutine(FadeOutLerp(confirmationUI));
        StartCoroutine(FadeInLerp(namingUI));
        
        foreach (GameObject slime in Camera.main.GetComponent<FocusOnSlime>().slimeTypes)
        {
            if (slime.name != Camera.main.GetComponent<FocusOnSlime>().target.name)
                slime.SetActive(false);
        }
    }

    // This is the method called by the "Nah..." button when selecting your slime.
    public void ViewAllSlimes()
    {
        Camera.main.GetComponent<FocusOnSlime>().target = null;
        StartCoroutine(FadeOutLerp(confirmationUI));
        StartCoroutine(FadeInLerp(promptUI));
    }

    // This is the method called by the Name input when hitting enter.
    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().playerSlimeType = Camera.main.GetComponent<FocusOnSlime>().target.name.ToString().Replace(" Slime", "");
        gameManager.GetComponent<GameManager>().playerSlimeName = slimeNameField.text.ToString();

        SceneManager.LoadScene("Ranch");
        DontDestroyOnLoad(gameManager);
    }

    #endregion

    #region Fade Lerps

    // Lerp used for fading out UI elements with CanvasGroup Components.
    public IEnumerator FadeOutLerp(CanvasGroup ui)
    {
        ui.interactable = false;
        ui.blocksRaycasts = false;

        for (int i = 0; i < 10; i++)
        {
            ui.alpha -= 0.1f;
            yield return new WaitForEndOfFrame();
        }

        ui.alpha = 0f;
    }

    // Lerp used for fading in UI elements with CanvasGroup Components.
    public IEnumerator FadeInLerp(CanvasGroup ui)
    {
        for (int i = 0; i < 10; i++)
        {
            ui.alpha += 0.1f;
            yield return new WaitForEndOfFrame();
        }

        ui.interactable = true;
        ui.blocksRaycasts = true;

        ui.alpha = 1f;
    }

    #endregion
}
