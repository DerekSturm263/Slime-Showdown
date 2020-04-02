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
    private GameObject slimeConfirmationButtonLayout;
    private GameObject slimeNameText;
    private GameObject slimeNameInputField;

    private void Start()
    {
        eventSystem = EventSystem.current;

        pickSlimeText = GameObject.FindGameObjectWithTag("ChooseSlimePickSlimeText");
        slimeConfirmationText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeConfirmationText");
        slimeConfirmationButtonLayout = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeConfirmationButtonLayout");
        slimeNameText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameText");
        slimeNameInputField = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameInputField");
    }

    // Called by the yeah button.
    public void Yeah()
    {
        slimeConfirmationButtonLayout.GetComponent<CanvasGroup>().interactable = false;
        Invoke("SelectInputField", 0.5f);

        slimeNameText.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameText_fadeIn");
        slimeNameInputField.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameInputField_floatIn");
        slimeConfirmationText.GetComponent<Animation>().Play("ui_chooseSlime_slimeConfirmationText_fadeOut");
        slimeConfirmationButtonLayout.GetComponent<Animation>().Play("ui_chooseSlime_slimeConfirmationButtonLayout_floatOut");
    }

    // Called by the nah button.
    public void Nah()
    {
        slimeConfirmationButtonLayout.GetComponent<CanvasGroup>().interactable = false;
        Deselect();

        pickSlimeText.GetComponent<Animation>().Play("ui_chooseSlime_pickSlimeText_fadeIn");
        slimeConfirmationText.GetComponent<Animation>().Play("ui_chooseSlime_slimeConfirmationText_fadeOut");
        slimeConfirmationButtonLayout.GetComponent<Animation>().Play("ui_chooseSlime_slimeConfirmationButtonLayout_floatOut");

        Camera.main.GetComponent<FocusOnSlime>().cameraTarget = null;
    }

    // Called by the naming input field when you press enter.
    public void FinishName()
    {
        slimeNameInputField.GetComponent<CanvasGroup>().interactable = false;

        slimeNameText.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameText_fadeOut");
        slimeNameInputField.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameInputField_floatOut");

        Invoke("LoadRanchScene", 1f);
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
