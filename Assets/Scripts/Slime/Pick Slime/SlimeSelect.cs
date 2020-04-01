using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSelect : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void OnMouseDown()
    {
        if (Camera.main.transform.position.z < -9.9f || Camera.main.transform.position.z > -4.1f)
        {
            // Moves the camera to the slime the player clicked on.
            Camera.main.GetComponent<FocusOnSlime>().target = gameObject;

            // Animate the UI Elements.
            GameObject.FindGameObjectWithTag("PromptUI").GetComponent<Animation>().Play("pickSlimePrompt_fadeOut");
            GameObject.FindGameObjectWithTag("ConfirmationUI").GetComponent<Animation>().Play("slimePickConfirmation_fadeIn");
            GameObject.FindGameObjectWithTag("ConfirmationButtons").GetComponent<Animation>().Play("slimePickButtons_floatUp");

            Invoke("Select", 0.01f);
        }
    }

    private void Select()
    {
        // Selects the yeah button.
        gameManager.GetComponent<GameManager>().eventSystem.SetSelectedGameObject(GameObject.FindGameObjectWithTag("YeahButton"));
    }
}
