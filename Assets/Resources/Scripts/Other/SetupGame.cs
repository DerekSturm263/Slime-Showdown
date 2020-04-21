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

        // Give the player the move punch.
        gameManager.GetComponent<GameManager>().PlayerMoves.Add(gameManager.GetComponent<GameManager>().MoveRoll);
    }
}
