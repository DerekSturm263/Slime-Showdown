using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    private GameObject gameManager;

    private enum State
    {
        Idle, Moving
    }
    private State animState = State.Idle;

    private enum Direction
    {
        Up, Down, Left, Right
    }
    private Direction animDirection = Direction.Down;

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
        animController.runtimeAnimatorController = Resources.Load(animName) as RuntimeAnimatorController;

        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        #region Position Changing

        if (Input.GetButton("Run"))
             moveSpeed = runSpeed;
        else
             moveSpeed = walkSpeed;

        moveAmount.x = Mathf.Lerp(0f, Input.GetAxis("Horizontal") * moveSpeed, Mathf.Abs(Input.GetAxis("Horizontal")));
        moveAmount.y = Mathf.Lerp(0f, Input.GetAxis("Vertical") * moveSpeed, Mathf.Abs(Input.GetAxis("Vertical")));
        
        rb2D.velocity = moveAmount;

        #endregion

        #region Animation States

        if (moveAmount.x == 0f && moveAmount.y == 0f)
            animState = State.Idle;
        else
            animState = State.Moving;

        if (moveAmount.y > 0f)
            animDirection = Direction.Up;
        else if (moveAmount.y < 0f)
            animDirection = Direction.Down;
        else if (moveAmount.x < 0f)
            animDirection = Direction.Left;
        else if (moveAmount.x > 0f)
            animDirection = Direction.Right;

        animController.SetInteger("State", (int) animState);
        animController.SetInteger("Direction", (int) animDirection);

        #endregion
    }
}
