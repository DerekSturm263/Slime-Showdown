using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject gameManager;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GameController").Length == 0)
        {
            GameObject newMainCamera = Instantiate(mainCamera);
            GameObject newGameManager = Instantiate(gameManager);

            DontDestroyOnLoad(newMainCamera);
            DontDestroyOnLoad(newGameManager);
        }

        MoveClass punch = new MoveClass("Punch", "Normal", "NONE", "None", 1);

        gameManager.GetComponent<GameManager>().PossibleMoves.Add(punch);
        gameManager.GetComponent<GameManager>().PlayerMoves[0] = gameManager.GetComponent<GameManager>().PossibleMoves[0];
    }
}
