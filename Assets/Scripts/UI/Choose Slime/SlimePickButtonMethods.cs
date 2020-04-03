using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlimePickButtonMethods : MonoBehaviour
{
    private GameObject gameManager;
    EventSystem eventSystem;

    private GameObject pickSlimeText;
    private GameObject slimeConfirmationText;
    private GameObject slimeConfirmationButtonLayout;
    private GameObject slimeNameText;
    private GameObject slimeNameRedoText;
    private GameObject slimeNameInputField;
    private GameObject slimeNameInputFieldText;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        eventSystem = EventSystem.current;

        pickSlimeText = GameObject.FindGameObjectWithTag("ChooseSlimePickSlimeText");
        slimeConfirmationText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeConfirmationText");
        slimeConfirmationButtonLayout = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeConfirmationButtonLayout");
        slimeNameText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameText");
        slimeNameRedoText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameRedoText");
        slimeNameInputField = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameInputField");
        slimeNameInputFieldText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameInputFieldText");
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
        if (slimeNameInputFieldText.GetComponent<Text>().text.Trim().Length > 0)
        {
            if (slimeNameText.GetComponent<CanvasGroup>().alpha == 1)
                slimeNameText.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameText_fadeOut");
            else
                slimeNameRedoText.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameRedoText_fadeOut");



            slimeNameInputField.GetComponent<CanvasGroup>().interactable = false;
            slimeNameInputField.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameInputField_floatOut");

            gameManager.GetComponent<GameManager>().playerSlimeColor = Camera.main.GetComponent<FocusOnSlime>().cameraTarget.name.Replace(" Slime", "");
            gameManager.GetComponent<GameManager>().playerSlimeName = slimeNameInputFieldText.GetComponent<Text>().text;

            Invoke("LoadRanchScene", 1f);
        }
        else
        {
            if (slimeNameRedoText.GetComponent<CanvasGroup>().alpha == 0)
            {
                slimeNameText.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameText_fadeOut");
                slimeNameRedoText.GetComponent<Animation>().Play("ui_chooseSlime_slimeNameRedoText_fadeIn");
            }
        }
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
        DontDestroyOnLoad(gameManager);
    }
}
