using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSun : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0f, 0f, gameManagerScript.sunRot);
    }
}
