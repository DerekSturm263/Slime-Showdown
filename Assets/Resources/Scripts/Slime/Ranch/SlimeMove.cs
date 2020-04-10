using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.Tilemaps;

public class SlimeMove : MonoBehaviour
{
    private GameObject gameManager;

    public enum State
    {
        Idle, Moving
    }

    public enum Direction
    {
        Up, Down, Left, Right
    }

    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 1.5f;

    private float moveSpeed;
    private Vector2 moveAmount;

    private Rigidbody2D rb2D;
    private Animator animController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        animController = GetComponent<Animator>();

        string animName = "Animations/Slime/Ranch/" + gameManager.GetComponent<GameManager>().playerSlimeColor + " Slime/slime_" + gameManager.GetComponent<GameManager>().playerSlimeColor.ToLower() + "_ranch";

        Debug.Log(animName);

        animController.runtimeAnimatorController = Resources.Load(animName) as RuntimeAnimatorController;

        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
         if (Input.GetButton("Run"))
             moveSpeed = runSpeed;
         else
             moveSpeed = walkSpeed;

         moveAmount.x = Mathf.Lerp(0, Input.GetAxis("Horizontal") * moveSpeed, Mathf.Abs(Input.GetAxis("Horizontal")));
         moveAmount.y = Mathf.Lerp(0, Input.GetAxis("Vertical") * moveSpeed, Mathf.Abs(Input.GetAxis("Vertical")));

         rb2D.velocity = moveAmount;
    }
}
