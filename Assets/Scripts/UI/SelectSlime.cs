using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSlime : MonoBehaviour
{
    // These are all of the slimes that exist in the scene.
    [HideInInspector]
    public GameObject[] slimeTypes;

    // This is the slime that the player has selected.
    [HideInInspector]
    public GameObject highlightedSlime;

    private void Start()
    {
        // Generates a list of every slime type and selects the pink slime by default.
        slimeTypes = GameObject.FindGameObjectsWithTag("Slime");
        highlightedSlime = slimeTypes[8];
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") > 0)
        {

        }
        else if (Input.GetAxis("Vertical") < 0)
        {

        }

        if (Input.GetAxis("Horizontal") < 0)
        {

        }
        else if (Input.GetAxis("Horizontal") > 0)
        {

        }
    }
}
