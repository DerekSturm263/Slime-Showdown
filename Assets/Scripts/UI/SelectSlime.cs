using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectSlime : MonoBehaviour
{
    EventSystem eventSystem;

    // These are all of the slimes that exist in the scene.
    [HideInInspector]
    public GameObject[] slimeTypes;

    // This is the slime that the player has selected.
    [HideInInspector]
    public GameObject highlightedSlime;

    private GameObject pickSlimeText;
    private GameObject slimeConfirmationText;
    private GameObject slimeConfirmationButtons;
    private GameObject slimeNameText;
    private GameObject slimeNameInputField;
    private GameObject yeahButton;

    Ray mouseRay;
    RaycastHit mouseRayHit;

    private void Start()
    {
        eventSystem = EventSystem.current;

        // Generates a list of every slime type and selects the pink slime by default.
        slimeTypes = GameObject.FindGameObjectsWithTag("Slime");
        highlightedSlime = slimeTypes[8];

        pickSlimeText = GameObject.FindGameObjectWithTag("PromptUI");
        slimeConfirmationText = GameObject.FindGameObjectWithTag("ConfirmationUI");
        slimeConfirmationButtons = GameObject.FindGameObjectWithTag("ConfirmationButtons");
        slimeNameText = GameObject.FindGameObjectWithTag("NamingUI");
        slimeNameInputField = GameObject.FindGameObjectWithTag("SlimeNameField");
        yeahButton = GameObject.FindGameObjectWithTag("YeahButton");
    }

    private void Update()
    {
        if (Camera.main.transform.position.z < -9.9f)
        {
            #region Mouse Selection

            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseRay, out mouseRayHit))
            {
                if (mouseRayHit.collider.gameObject.tag.Equals("Slime"))
                {
                    highlightedSlime = mouseRayHit.collider.gameObject;
                }
            }

            #endregion

            #region Keyboard Selection

            if (Input.GetAxis("Vertical") > 0 && highlightedSlime.GetComponent<SlimeData>().slimeUp != null)
            {
                highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeUp;
            }
            else if (Input.GetAxis("Vertical") < 0 && highlightedSlime.GetComponent<SlimeData>().slimeDown != null)
            {
                highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeDown;
            }

            if (Input.GetAxis("Horizontal") < 0 && highlightedSlime.GetComponent<SlimeData>().slimeLeft != null)
            {
                highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeLeft;
            }
            else if (Input.GetAxis("Horizontal") > 0 && highlightedSlime.GetComponent<SlimeData>().slimeRight != null)
            {
                highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeRight;
            }

            #endregion

            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
                FocusOnSlime();
        }
    }

    private void FocusOnSlime()
    {
        Camera.main.GetComponent<FocusOnSlime>().cameraTarget = highlightedSlime;
        slimeConfirmationButtons.GetComponent<CanvasGroup>().interactable = true;
        Invoke("SelectYeahButton", 0.5f);

        pickSlimeText.GetComponent<Animation>().Play("pickSlimePrompt_fadeOut");
        slimeConfirmationText.GetComponent<Animation>().Play("slimePickConfirmation_fadeIn");
        slimeConfirmationButtons.GetComponent<Animation>().Play("slimePickButtons_floatUp");
    }

    private void SelectYeahButton()
    {
        eventSystem.SetSelectedGameObject(yeahButton);
    }
}
