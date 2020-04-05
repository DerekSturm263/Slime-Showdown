using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SlimeMove : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 1f;
    [SerializeField]
    private float runSpeed = 1.5f;

    private float moveSpeed;
    private Vector2 moveAmount;

    private Rigidbody2D rb2D;

    private void Start()
    {
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
