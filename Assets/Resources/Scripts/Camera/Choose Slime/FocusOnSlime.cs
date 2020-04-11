using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusOnSlime : MonoBehaviour
{
    // This is the gameobject that the camera will fly to when selecting a slime.
    [HideInInspector]
    public GameObject cameraTarget;

    // This is the position that the camera will return to when the player zooms back out.
    public Vector3 returnPosition;

    public float cameraSpeed;

    // Controls the camera movement.
    private void FixedUpdate()
    {
        if (cameraTarget != null)
            transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position - new Vector3(0, 0, 4), cameraSpeed * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, returnPosition, cameraSpeed * Time.deltaTime);
    }
}
