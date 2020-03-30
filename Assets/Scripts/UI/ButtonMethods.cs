using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMethods : MonoBehaviour
{
    private GameObject gameManager;

    #region Title Screen Stuff

    private GameObject title;
    private GameObject buttons;

    #endregion

    #region Slime Picking Stuff

    private CanvasGroup confirmationUI;
    private CanvasGroup promptUI;
    private CanvasGroup namingUI;

    private Text slimeNameField;

    #endregion

    private void Start()
    {
        try
        {
            gameManager = GameObject.FindGameObjectWithTag("GameController");

            title = GameObject.FindGameObjectWithTag("GameLogo");
            buttons = GameObject.FindGameObjectWithTag("Buttons");

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
        StartCoroutine(MoveLerp(buttons, 0f, -4f, 0f, 0.5f));
        StartCoroutine(MoveLerp(title, 0f, 5.5f, 0f, 0.5f));

        StartCoroutine(LoadSceneDelay("PickSlime", 0.5f));
    }

    public void ToCredits()
    {
        StartCoroutine(LoadSceneDelay("Credits", 0.5f));
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
    private IEnumerator FadeOutLerp(CanvasGroup ui)
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
    private IEnumerator FadeInLerp(CanvasGroup ui)
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

    #region Move Lerps

    // Lerp used for moving UI Elements.
    private IEnumerator MoveLerp(GameObject gameObject, float xDist, float yDist, float zDist, float seconds)
    {
        float newPosX = gameObject.transform.position.x + xDist;
        float newPosY = gameObject.transform.position.y + yDist;
        float newPosZ = gameObject.transform.position.z + zDist;

        float elapsedTime = 0;

        while (elapsedTime < seconds)
        {
            gameObject.transform.position = new Vector3(Mathf.Lerp(gameObject.transform.position.x, newPosX, elapsedTime / seconds),
                                                        Mathf.Lerp(gameObject.transform.position.y, newPosY, elapsedTime / seconds),
                                                        Mathf.Lerp(gameObject.transform.position.z, newPosZ, elapsedTime / seconds));
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        gameObject.transform.position = new Vector3(newPosX, newPosY, newPosZ);
    }

    #endregion

    private IEnumerator LoadSceneDelay(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
