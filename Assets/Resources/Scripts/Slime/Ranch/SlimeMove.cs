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

    [SerializeField] private float walkSpeed = 1f;
    [SerializeField] private float runSpeed = 1.5f;

    private float moveSpeed;
    private Vector2 moveAmount;

    private Rigidbody2D rb2D;
    private Animator animController;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");

        animController = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        string animName = "Animations/Slime/Ranch/" + gameManager.GetComponent<GameManager>().playerSlimeColor + " Slime/Idle/slime_" + gameManager.GetComponent<GameManager>().playerSlimeColor.ToLower() + "_ranch";
        animController.runtimeAnimatorController = Resources.Load(animName) as RuntimeAnimatorController;
    }

    private void FixedUpdate()
    {
        #region Position Changing

        // Sets the movement speed to either walking or running depending on whether the player is hitting the "Run" button.
        moveSpeed = ( Input.GetButton("Run") ) ? runSpeed : walkSpeed;

        // Sets the x and the y movement based on Lerp values dependent on GetAxis and the movement speed.
        moveAmount.x = Mathf.Lerp(0f, Input.GetAxis("Horizontal") * moveSpeed, Mathf.Abs(Input.GetAxis("Horizontal")));
        moveAmount.y = Mathf.Lerp(0f, Input.GetAxis("Vertical") * moveSpeed, Mathf.Abs(Input.GetAxis("Vertical")));
        
        // Sets the velocity to moveAmount.
        rb2D.velocity = moveAmount;

        #endregion

        #region Animation States

        // Sets whether you're idle or not.
        animState = ( moveAmount.magnitude == 0f ) ? State.Idle : State.Moving;

        // Sets the direction you're moving in.
        if (Mathf.Abs(moveAmount.x) > Mathf.Abs(moveAmount.y))
            animDirection = ( moveAmount.x < 0 ) ? Direction.Left : Direction.Right;
        else if (Mathf.Abs(moveAmount.x) < Mathf.Abs(moveAmount.y))
            animDirection = ( moveAmount.y < 0 ) ? Direction.Down : Direction.Up;

        // Converts them into animation states.
        animController.SetInteger("State", (int) animState);
        animController.SetInteger("Direction", (int) animDirection);

        #endregion
    }
}
