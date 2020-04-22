using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject gameManager;

    private void Start()
    {
        // This will only run and set up the game once to avoid duplicates.
        if (GameObject.FindGameObjectsWithTag("GameController").Length == 0)
        {
            MusicPlayer.Initialize();
            MusicPlayer.ChangeVolume(0.5f);

            SoundPlayer.Initialize();
            SoundPlayer.ChangeVolume(0.5f);

            GameObject newMainCamera = Instantiate(mainCamera);
            GameObject newGameManager = Instantiate(gameManager);

            DontDestroyOnLoad(newMainCamera);
            DontDestroyOnLoad(newGameManager);

            DontDestroyOnLoad(MusicPlayer.audioSource.gameObject);
            DontDestroyOnLoad(SoundPlayer.audioSource.gameObject);

            // Gives the player the move Roll.
            gameManager.GetComponent<GameManager>().PlayerMoves[0] = (gameManager.GetComponent<GameManager>().MoveRoll);
            gameManager.GetComponent<GameManager>().PlayerMoves[1] = (gameManager.GetComponent<GameManager>().MoveRoll);
            gameManager.GetComponent<GameManager>().PlayerMoves[2] = (gameManager.GetComponent<GameManager>().MoveRoll);

            Debug.Log("Gave the player the move Roll.");
        }

        if (MusicPlayer.CurrentTrack() != MusicPlayer.Track("music_mainTheme"))
            MusicPlayer.Play("music_mainTheme");
    }
}
