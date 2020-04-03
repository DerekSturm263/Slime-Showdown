using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RanchButtonMethods : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene("Battle");
        DontDestroyOnLoad(gameManager);
    }
}
