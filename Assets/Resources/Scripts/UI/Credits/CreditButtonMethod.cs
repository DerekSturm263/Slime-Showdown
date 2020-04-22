using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CreditButtonMethod : MonoBehaviour
{
    private GameObject backButton;

    private void Start()
    {
        backButton = GameObject.FindGameObjectWithTag("ShopBackButton");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TitleButton();
    }

    public void TitleButton()
    {
        SoundPlayer.Play("sound_ui_select");

        if (Camera.main.GetComponent<FocusOnSlime>().cameraTarget == null)
        {
            backButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");

            Camera.main.GetComponent<FocusOnSlime>().enabled = false;
            Camera.main.GetComponent<MoveLerp>().enabled = true;

            Invoke("LoadTitle", 0.5f);
        }
        else
        {
            Camera.main.GetComponent<FocusOnSlime>().cameraTarget = null;

            foreach (GameObject g in GameObject.FindGameObjectsWithTag(EventSystem.current.GetComponent<SelectSlime>().highlightedSlime.name))
            {
                if ((int)g.GetComponent<CreditsUIElement>().thisUI == 0)
                    g.GetComponent<CreditsUIElement>().FadeIn();
                else
                    g.GetComponent<CreditsUIElement>().FadeOut();
            }
        }
    }

    private void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
