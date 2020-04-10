using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayVersionNumber : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        GetComponent<Text>().text = "Version " + gameManager.GetComponent<GameManager>().version;
    }
}
