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

        ListenForEvents(fullscreenToggle.onValueChanged, false);
        ListenForEvents(musicSlider.onValueChanged, false);
        ListenForEvents(soundSlider.onValueChanged, false);

        fullscreenToggle.isOn = gameManager.isFullscreen;
        musicSlider.value = gameManager.musicVolume;
        soundSlider.value = gameManager.soundVolume;

        ListenForEvents(fullscreenToggle.onValueChanged, true);
        ListenForEvents(musicSlider.onValueChanged, true);
        ListenForEvents(soundSlider.onValueChanged, true);
    }

    // Called by the toggle button on value change.
    public void ToggleFullscreen()
    {
        SoundPlayer.Play("sound_ui_select");

        gameManager.isFullscreen = !gameManager.isFullscreen;
        Screen.fullScreen = gameManager.isFullscreen;
    }

    // Called by the music slider on value change.
    public void ChangeMusicVolume()
    {
        SoundPlayer.Play("sound_ui_select");

        gameManager.musicVolume = musicSlider.value;
        MusicPlayer.ChangeVolume(musicSlider.value);
    }

    // Called by the sound slider on value change.
    public void ChangeSoundVolume()
    {
        SoundPlayer.Play("sound_ui_select");

        gameManager.soundVolume = soundSlider.value;
        SoundPlayer.ChangeVolume(soundSlider.value);
    }

    // Called by the control remapping button.
    public void GoToButtonRemap()
    {
        SoundPlayer.Play("sound_ui_select");

        // Code to open control remap screen.
    }

    // Called by the back button if you came from the title screen.
    public void BackToTitle()
    {
        SoundPlayer.Play("sound_ui_select");

        background.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");
        backButton.GetComponent<Animation>().Play("ui_ranch_shopBackButton_floatOut");
        optionsElements.GetComponent<Animation>().Play("ui_ranch_shopBackground_fadeOut");

        Invoke("LoadTitle", 0.5f);
    }

    private void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    // Used to "mute" or "unmute" an eventListener for a UI element.
    public void ListenForEvents(UnityEngine.Events.UnityEventBase ev, bool isMute)
    {
        for (int i = 0; i < ev.GetPersistentEventCount(); i++)
        {
            if (!isMute)    ev.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.Off);
            else           ev.SetPersistentListenerState(i, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
        }
    }
}
