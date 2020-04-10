using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGold : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    private void Update()
    {
        GetComponent<Text>().text = "$" + gameManager.GetComponent<GameManager>().goldCount.ToString();
    }
}
