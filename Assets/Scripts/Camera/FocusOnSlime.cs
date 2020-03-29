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
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        float currentZ = transform.position.z;

        float targetX;
        float targetY;
        float targetZ;

        if (target != null)
        {
            targetX = target.transform.position.x;
            targetY = target.transform.position.y;
            targetZ = target.transform.position.z - 4;
        }
        else
        {
            targetX = returnPosition.x;
            targetY = returnPosition.y;
            targetZ = returnPosition.z;
        }

        // Lerps the camera position to a new position based on the current and target positions.
        transform.position = new Vector3(Mathf.Lerp(currentX, targetX, 5 * Time.deltaTime), Mathf.Lerp(currentY, targetY, 5 * Time.deltaTime), Mathf.Lerp(currentZ, targetZ, 5 * Time.deltaTime));
    }
}
