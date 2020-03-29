using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSelect : MonoBehaviour
{
    private CanvasGroup confirmationUI;

    private void Start()
    {
        confirmationUI = GameObject.FindGameObjectWithTag("ConfirmationUI").GetComponent<CanvasGroup>();
    }

    private void OnMouseDown()
    {
        Camera.main.GetComponent<FocusOnSlime>().target = gameObject;

        if (confirmationUI.alpha == 0)
            StartCoroutine(FadeInLerp(confirmationUI));
    }

    // Lerp used for fading in UI elements with CanvasGroup Components.
    public IEnumerator FadeInLerp(CanvasGroup ui)
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
}
