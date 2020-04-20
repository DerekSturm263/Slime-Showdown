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

        // Define each of the moves.
        MoveClass punch = new MoveClass("Punch", "Normal", "NONE", "None", 1);

        // Add each of those moves to a list of possible moves.
        gameManager.GetComponent<GameManager>().PossibleMoves.Clear();
        gameManager.GetComponent<GameManager>().PossibleMoves.Add(punch);

        // Give the player the move punch.
        gameManager.GetComponent<GameManager>().PlayerMoves[0] = punch;
    }
}
