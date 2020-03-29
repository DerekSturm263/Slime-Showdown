using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMethods : MonoBehaviour
{
    private CanvasGroup confirmationUI;

    private void Start()
    {
        confirmationUI = GameObject.FindGameObjectWithTag("ConfirmationUI").GetComponent<CanvasGroup>();
    }

    public void PickSlime()
    {

    }

    public void ViewAllSlimes()
    {
        Camera.main.GetComponent<FocusOnSlime>().target = null;
        StartCoroutine(FadeOutLerp(confirmationUI));
    }

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
}
