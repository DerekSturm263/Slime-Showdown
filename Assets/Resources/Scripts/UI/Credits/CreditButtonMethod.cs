using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButtonMethod : MonoBehaviour
{
    private GameObject backButton;

    private void Start()
    {
        backButton = GameObject.FindGameObjectWithTag("ShopBackButton");
    }

    public void TitleButton()
    {
        backButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");

        Camera.main.GetComponent<FocusOnSlime>().enabled = false;
        Camera.main.GetComponent<MoveLerp>().enabled = true;

        Invoke("LoadTitle", 0.5f);
    }

    private void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
