using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSelect : MonoBehaviour
{
    private CanvasGroup confirmationUI;
    private CanvasGroup promptUI;

    private void Start()
    {
        confirmationUI = GameObject.FindGameObjectWithTag("ConfirmationUI").GetComponent<CanvasGroup>();
        promptUI = GameObject.FindGameObjectWithTag("PromptUI").GetComponent<CanvasGroup>();
    }

    private void OnMouseDown()
    {
        // Focuses on the slime the player clicks on.
        Camera.main.GetComponent<FocusOnSlime>().target = gameObject;

        if (confirmationUI.alpha == 0)
        {
            StartCoroutine(FadeInLerp(confirmationUI));
            StartCoroutine(FadeOutLerp(promptUI));
        }
    }

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
}
