using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUIElement : MonoBehaviour
{
    public enum UIType
    {
        OverviewElement, FocusedElement
    }
    public UIType thisUI;

    public void FadeOut()
    {
        GetComponent<Animation>().Play("ui_ranch_shopContent_fadeOut");
    }

    public void FadeIn()
    {
        GetComponent<Animation>().Play("ui_ranch_shopContent_fadeIn");
    }
}
