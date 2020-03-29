using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeMove : MonoBehaviour
{
    Vector2 move = new Vector2(0, 0);

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Ranch")
		{
			if (Input.GetAxisRaw("Horizontal")>0 || Input.GetAxisRaw("Horizontal") <= 0)
			{
                move = new Vector2(transform.position.x + Input.GetAxisRaw("Horizontal"), 0);
			}
            if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") <= 0)
            {
                move = new Vector2(0, transform.position.y + Input.GetAxisRaw("Vertical"));
            }
            transform.position = move;

        }
    }
}
