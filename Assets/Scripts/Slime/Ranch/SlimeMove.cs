using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SlimeMove : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    private float moveSpeed;
    private Vector2 moveAmount;

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name.Equals("Ranch"))
        {
            if (Input.GetButton("Run"))
                moveSpeed = runSpeed;
            else
                moveSpeed = walkSpeed;

            moveAmount.x = Mathf.Lerp(0, Input.GetAxis("Horizontal") * moveSpeed, Mathf.Abs(Input.GetAxis("Horizontal")));
            moveAmount.y = Mathf.Lerp(0, Input.GetAxis("Vertical") * moveSpeed, Mathf.Abs(Input.GetAxis("Vertical")));

            transform.position += (Vector3) moveAmount;
        }
    }
}
