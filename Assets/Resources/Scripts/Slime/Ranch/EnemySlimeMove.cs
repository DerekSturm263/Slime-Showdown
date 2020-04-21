using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySlimeMove : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject playerSlime;

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

    private float moveX, moveY;
    private float moveSpeed = 1f;
    private Vector2 moveAmount;

    private Rigidbody2D rb2D;
    private Animator animController;

    private void Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Ranch"))
            this.enabled = false;

        gameManager = GameObject.FindGameObjectWithTag("GameController");
        playerSlime = GameObject.FindGameObjectWithTag("RanchBattleSlime");

        animController = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        string animName = "Animations/Slime/Ranch/" + GetComponent<Player>().color.Replace(" Type", "") + " Slime/Idle/slime_" + GetComponent<Player>().color.Replace(" Type", "").ToLower() + "_ranch";
        animController.runtimeAnimatorController = Resources.Load(animName) as RuntimeAnimatorController;

        StartCoroutine(Movement());
    }

    private void Update()
    {
        #region Position Changing

        // Sets the velocity to based on distance from the player.
        if (Vector3.Distance(transform.position, playerSlime.transform.position) > 3.5f)
            moveAmount = new Vector2(moveX, moveY);
        else
            moveAmount = new Vector3(playerSlime.transform.position.x - transform.position.x, playerSlime.transform.position.y - transform.position.y, 0).normalized * 1.75f;

        rb2D.velocity = moveAmount * moveSpeed;

        #endregion

        #region Animation States

        // Sets whether the enemy slime is idle or not.
        animState = (moveAmount.magnitude == 0f) ? State.Idle : State.Moving;

        // Sets the direction the enemy slime is moving in.
        if (Mathf.Abs(moveAmount.x) > Mathf.Abs(moveAmount.y))
            animDirection = (moveAmount.x < 0) ? Direction.Left : Direction.Right;
        else if (Mathf.Abs(moveAmount.x) < Mathf.Abs(moveAmount.y))
            animDirection = (moveAmount.y < 0) ? Direction.Down : Direction.Up;

        // Converts them into animation states.
        animController.SetInteger("State", (int) animState);
        animController.SetInteger("Direction", (int) animDirection);

        #endregion
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            int random = Random.Range(0, 4);

            switch (random)
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
            }

            yield return new WaitForSeconds(Random.Range(0.75f, 1.25f));
        }
    }

    private void MoveUp()
    {
        moveY = Random.Range(0.75f, 1.25f);
    }
    private void MoveDown()
    {
        moveY = Random.Range(-0.75f, -1.25f);
    }
    private void MoveLeft()
    {
        moveX = Random.Range(-0.75f, -1.25f);
    }
    private void MoveRight()
    {
        moveX = Random.Range(0.75f, 1.25f);
    }
}
