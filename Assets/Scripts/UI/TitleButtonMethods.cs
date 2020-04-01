using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonMethods : MonoBehaviour
{
    private Animation logoAnim;
    private Animation buttonsAnim;

    private void Start()
    {
        logoAnim = GameObject.FindGameObjectWithTag("GameLogo").GetComponent<Animation>();
        buttonsAnim = GameObject.FindGameObjectWithTag("Buttons").GetComponent<Animation>();
    }

    public void StartGame()
    {
        logoAnim.Play("gameLogo_floatUp");
        buttonsAnim.Play("buttons_floatDown");

        StartCoroutine(LoadSceneDelay("PickSlime", 1f));
    }

    public void LoadCredits()
    {
        logoAnim.Play("gameLogo_floatUp");
        buttonsAnim.Play("buttons_floatDown");

        StartCoroutine(LoadSceneDelay("Credits", 1f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator LoadSceneDelay(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
