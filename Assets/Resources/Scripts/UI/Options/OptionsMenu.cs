using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject background;
    private GameObject backButton;
    private GameObject optionsElements;

    private Toggle fullscreenToggle;
    private Slider musicSlider;
    private Slider soundSlider;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        background = GameObject.FindGameObjectWithTag("ShopBackground");
        backButton = GameObject.FindGameObjectWithTag("ShopBackButton");
        optionsElements = GameObject.FindGameObjectWithTag("Options");

        fullscreenToggle = GameObject.FindGameObjectWithTag("FullscreenToggle").GetComponent<Toggle>();
        musicSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        soundSlider = GameObject.FindGameObjectWithTag("SoundSlider").GetComponent<Slider>();

        fullscreenToggle.isOn = gameManager.isFullscreen;
        musicSlider.value = gameManager.musicVolume;
        soundSlider.value = gameManager.soundVolume;
    }

    private void Update()
    {
        gameManager.musicVolume = musicSlider.value;
        gameManager.soundVolume = soundSlider.value;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        gameManager.isFullscreen = Screen.fullScreen;
    }

    public void GoToButtonRemap()
    {

    }

    public void BackToTitle()
    {
        backButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");
        background.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
        optionsElements.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        Invoke("LoadTitle", 0.5f);
    }

    public void CloseSettingsInRanch()
    {

    }

    private void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
