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
            MusicPlayer.Initialize();

            GameObject newMainCamera = Instantiate(mainCamera);
            GameObject newGameManager = Instantiate(gameManager);

            DontDestroyOnLoad(newMainCamera);
            DontDestroyOnLoad(newGameManager);
            DontDestroyOnLoad(MusicPlayer.audioSource.gameObject);
        }

        if (MusicPlayer.CurrentTrack() != MusicPlayer.Track("music_mainTheme"))
            MusicPlayer.Play("music_mainTheme");

        // Give the player the move Roll.
        gameManager.GetComponent<GameManager>().PlayerMoves[0] = (gameManager.GetComponent<GameManager>().MoveRoll);
        gameManager.GetComponent<GameManager>().PlayerMoves[1] = (gameManager.GetComponent<GameManager>().MoveRoll);
        gameManager.GetComponent<GameManager>().PlayerMoves[2] = (gameManager.GetComponent<GameManager>().MoveRoll);
        Debug.Log("Passed this point, move should be added");
    }
}
