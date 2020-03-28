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
       
        Scene currentScene = currentScene.Name;
        if(currentScene == "Ranch")
		{
			if (Input.getAxisRaw("Horizontal")>0 || Input.getAxisRaw("Horizontal") <= 0)
			{
                move = new Vector2(transform.position.x + Input.getAxisRaw("Horizontal"), 0);
			}
            if (Input.getAxisRaw("Vertical") > 0 || Input.getAxisRaw("Vertical") <= 0)
            {
                move = new Vector2(0, transform.position.y + Input.getAxisRaw("Vertical"));
            }
            transform.position = move;

        }
    }
}
