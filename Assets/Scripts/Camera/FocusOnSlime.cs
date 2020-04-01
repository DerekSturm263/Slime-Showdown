using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnSlime : MonoBehaviour
{
    // This is the slime that the camera will focus on.
    [HideInInspector]
    public GameObject target;

    private Vector3 returnPosition;

    [HideInInspector]
    public GameObject[] slimeTypes;

    private void Start()
    {
        slimeTypes = GameObject.FindGameObjectsWithTag("Slime");

        returnPosition = transform.position;
    }

    private void FixedUpdate()
    {
        // Lerps the camera position to a new position based on the current and target positions.
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, target.transform.position - new Vector3(0, 0, 4), 5 * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, returnPosition, 5 * Time.deltaTime);

    }
}
