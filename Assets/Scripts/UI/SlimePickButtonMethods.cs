using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SlimePickButtonMethods : MonoBehaviour
{
    EventSystem eventSystem;

    private GameObject pickSlimeText;
    private GameObject slimeConfirmationText;
    private GameObject slimeConfirmationButtons;
    private GameObject slimeNameText;
    private GameObject slimeNameInputField;

    private void Start()
    {
        eventSystem = EventSystem.current;

        pickSlimeText = GameObject.FindGameObjectWithTag("PromptUI");
        slimeConfirmationText = GameObject.FindGameObjectWithTag("ConfirmationUI");
        slimeConfirmationButtons = GameObject.FindGameObjectWithTag("ConfirmationButtons");
        slimeNameText = GameObject.FindGameObjectWithTag("NamingUI");
        slimeNameInputField = GameObject.FindGameObjectWithTag("SlimeNameField");
    }

    // Called by the yeah button.
    public void Yeah()
    {
        slimeConfirmationButtons.GetComponent<CanvasGroup>().interactable = false;
        Invoke("SelectInputField", 0.5f);

        slimeNameText.GetComponent<Animation>().Play("slimeNamePrompt_fadeIn");
        slimeNameInputField.GetComponent<Animation>().Play("slimeNameInput_floatUp");
        slimeConfirmationText.GetComponent<Animation>().Play("slimePickConfirmation_fadeOut");
        slimeConfirmationButtons.GetComponent<Animation>().Play("slimePickButtons_floatDown");
    }

    // Called by the nah button.
    public void Nah()
    {
        slimeConfirmationButtons.GetComponent<CanvasGroup>().interactable = false;
        Deselect();

        pickSlimeText.GetComponent<Animation>().Play("pickSlimePrompt_fadeIn");
        slimeConfirmationText.GetComponent<Animation>().Play("slimePickConfirmation_fadeOut");
        slimeConfirmationButtons.GetComponent<Animation>().Play("slimePickButtons_floatDown");

        Camera.main.GetComponent<FocusOnSlime>().cameraTarget = null;
    }

    // Called by the naming input field when you press enter.
    public void FinishName()
    {
        slimeNameInputField.GetComponent<CanvasGroup>().interactable = false;

        slimeNameText.GetComponent<Animation>().Play("slimeNamePrompt_fadeOut");
        slimeNameInputField.GetComponent<Animation>().Play("slimeNameInput_floatDown");

        Invoke("LoadSlimePickScene", 1f);
    }

    private void SelectInputField()
    {
        eventSystem.SetSelectedGameObject(slimeNameInputField);
    }

    private void Deselect()
    {
        eventSystem.SetSelectedGameObject(null);
    }

    private void LoadRanchScene()
    {
        SceneManager.LoadScene("Ranch");
    }
}
