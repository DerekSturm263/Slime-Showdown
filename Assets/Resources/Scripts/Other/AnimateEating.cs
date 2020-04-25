using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateEating : MonoBehaviour
{
    private GameObject gameManager;

    private Image image;
    private SpriteRenderer sprtRndr;
    private Animator animController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        string animName = "Animations/Slime/Ranch/" + gameManager.GetComponent<GameManager>().playerSlimeColor + " Slime/Eating/slime_" + gameManager.GetComponent<GameManager>().playerSlimeColor.ToLower() + "_ranch_eatingController";

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
