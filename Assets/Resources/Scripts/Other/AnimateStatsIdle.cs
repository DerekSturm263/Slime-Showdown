using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateStatsIdle : MonoBehaviour
{
    private GameObject gameManager;

    private Image image;
    private SpriteRenderer sprtRndr;
    private Animator animController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        string animName = "Animations/Slime/Choose Slime/" + gameManager.GetComponent<GameManager>().playerSlimeColor + " Slime/Eating/slime_" + gameManager.GetComponent<GameManager>().playerSlimeColor.ToLower() + "_pickSlime";

        image = GetComponent<Image>();
        sprtRndr = GetComponent<SpriteRenderer>();
        animController = GetComponent<Animator>();

        animController.runtimeAnimatorController = Resources.Load(animName) as RuntimeAnimatorController;
    }

    private void Update()
    {
        image.sprite = sprtRndr.sprite;
    }
}
