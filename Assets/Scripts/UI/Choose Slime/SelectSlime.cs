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

    // For use with mouse selection.
    Ray mouseRay;
    RaycastHit mouseRayHit;

    // For use with keyboard/controller selection.
    bool usingAxisX = false;
    bool usingAxisY = false;

    private GameObject pickSlimeText;
    private GameObject slimeConfirmationText;
    private GameObject slimeConfirmationButtonLayout;
    private GameObject buttonYeah;
    private GameObject slimeNameText;
    private GameObject slimeNameInputField;

    private void Start()
    {
        eventSystem = EventSystem.current;

        // Generates a list of every slime type and selects the pink slime by default.
        slimeTypes = GameObject.FindGameObjectsWithTag("ChooseSlimeSlime");
        highlightedSlime = slimeTypes[8];

        pickSlimeText = GameObject.FindGameObjectWithTag("ChooseSlimePickSlimeText");
        slimeConfirmationText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeConfirmationText");
        slimeConfirmationButtonLayout = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeConfirmationButtonLayout");
        buttonYeah = GameObject.FindGameObjectWithTag("ChooseSlimeButtonYeah");
        slimeNameText = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameText");
        slimeNameInputField = GameObject.FindGameObjectWithTag("ChooseSlimeSlimeNameInputField");
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

            #region Keyboard/Controller Selection

            if (!usingAxisY)
            {
                if (Input.GetAxisRaw("Vertical") > 0 && highlightedSlime.GetComponent<SlimeData>().slimeUp != null)
                {
                    usingAxisY = true;
                    highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeUp;
                }
                else if (Input.GetAxisRaw("Vertical") < 0 && highlightedSlime.GetComponent<SlimeData>().slimeDown != null)
                {
                    usingAxisY = true;
                    highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeDown;
                }
            }

            if (!usingAxisX)
            {
                if (Input.GetAxisRaw("Horizontal") < 0 && highlightedSlime.GetComponent<SlimeData>().slimeLeft != null)
                {
                    usingAxisX = true;
                    highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeLeft;
                }
                else if (Input.GetAxisRaw("Horizontal") > 0 && highlightedSlime.GetComponent<SlimeData>().slimeRight != null)
                {
                    usingAxisX = true;
                    highlightedSlime = highlightedSlime.GetComponent<SlimeData>().slimeRight;
                }
            }

            if (Input.GetAxisRaw("Vertical") == 0)
            {
                usingAxisY = false;
            }
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                usingAxisX = false;
            }

            #endregion

            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Submit"))
                FocusOnSlime();
        }
    }

    private void FocusOnSlime()
    {
        Camera.main.GetComponent<FocusOnSlime>().cameraTarget = highlightedSlime;
        slimeConfirmationButtonLayout.GetComponent<CanvasGroup>().interactable = true;
        Invoke("SelectYeahButton", 0.5f);

        pickSlimeText.GetComponent<Animation>().Play("ui_chooseSlime_pickSlimeText_fadeOut");
        slimeConfirmationText.GetComponent<Animation>().Play("ui_chooseSlime_slimeConfirmationText_fadeIn");
        slimeConfirmationButtonLayout.GetComponent<Animation>().Play("ui_chooseSlime_slimeConfirmationButtonLayout_floatIn");
    }

    private void SelectYeahButton()
    {
        eventSystem.SetSelectedGameObject(buttonYeah);
    }
}
