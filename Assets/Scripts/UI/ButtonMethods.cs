using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonMethods : MonoBehaviour
{
    #region Title Variables

    private Animation gameLogoAnim;
    private Animation titleScreenButtonsAnim;

    #endregion

    #region Slime Picking Variables

    private GameObject gameManager;

    private Animation promptUIAnim;
    private Animation confirmationUIAnim;
    private Animation confirmationButtonsAnim;
    private Animation namingUIAnim;
    private Animation slimeNameFieldAnim;

    #endregion

    private void Start()
    {
        // Title scene elements.
        try
        {
            gameLogoAnim = GameObject.FindGameObjectWithTag("GameLogo").GetComponent<Animation>();
            titleScreenButtonsAnim = GameObject.FindGameObjectWithTag("Buttons").GetComponent<Animation>();
        }
        catch { }

        // Slime Picking scene elements.
        try
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController");

            promptUIAnim = GameObject.FindGameObjectWithTag("PromptUI").GetComponent<Animation>();
            confirmationUIAnim = GameObject.FindGameObjectWithTag("ConfirmationUI").GetComponent<Animation>();
            confirmationButtonsAnim = GameObject.FindGameObjectWithTag("ConfirmationButtons").GetComponent<Animation>();
            namingUIAnim = GameObject.FindGameObjectWithTag("NamingUI").GetComponent<Animation>();
            slimeNameFieldAnim = GameObject.FindGameObjectWithTag("SlimeNameField").GetComponent<Animation>();
        }
        catch { }
    }

    #region Title Methods

    public void ToSlimePick()
    {
        gameLogoAnim.Play("gameLogo_floatUp");
        titleScreenButtonsAnim.Play("buttons_floatDown");

        StartCoroutine(LoadSceneDelay("PickSlime", 0.5f));
    }

    public void ToCredits()
    {
        gameLogoAnim.Play("gameLogo_floatUp");
        titleScreenButtonsAnim.Play("buttons_floatDown");

        StartCoroutine(LoadSceneDelay("Credits", 0.5f));
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region Slime Picking Methods

    // This is the method called by the "Yeah!" button when selecting your slime.
    public void PickSlime()
    {
        confirmationUIAnim.Play("slimePickConfirmation_fadeOut");
        confirmationButtonsAnim.Play("slimePickButtons_floatDown");
        namingUIAnim.Play("slimeNamePrompt_fadeIn");
        slimeNameFieldAnim.Play("slimeNameInput_floatUp");

        Invoke("Select", 0.01f);

        foreach (GameObject slime in Camera.main.GetComponent<FocusOnSlime>().slimeTypes)
        {
            if (slime.name != Camera.main.GetComponent<FocusOnSlime>().target.name)
                slime.SetActive(false);
        }
    }

    // This is the method called by the "Nah..." button when selecting your slime.
    public void ViewAllSlimes()
    {
        confirmationUIAnim.Play("slimePickConfirmation_fadeOut");
        confirmationButtonsAnim.Play("slimePickButtons_floatDown");
        promptUIAnim.Play("pickSlimePrompt_fadeIn");

        Camera.main.GetComponent<FocusOnSlime>().target = null;
    }

    // This is the method called by the Name input when hitting enter.
    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().playerSlimeType = Camera.main.GetComponent<FocusOnSlime>().target.name.ToString().Replace(" Slime", "");
        gameManager.GetComponent<GameManager>().playerSlimeName = GameObject.FindGameObjectWithTag("SlimeName").GetComponent<Text>().text.ToString();

        SceneManager.LoadScene("Ranch");
        DontDestroyOnLoad(gameManager);
    }

    #endregion

    private void Select()
    {
        // Selects the yeah button by default.
        gameManager.GetComponent<GameManager>().eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("SlimeNameField"));
    }

    private IEnumerator LoadSceneDelay(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
