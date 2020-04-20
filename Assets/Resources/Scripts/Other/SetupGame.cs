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

         

        // Give the player the move punch.
         gameManager.GetComponent<GameManager>().PlayerMoves.Add(gameManager.GetComponent<GameManager>().MoveRoll);
    }
}
