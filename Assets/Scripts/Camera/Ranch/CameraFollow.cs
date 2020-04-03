using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cameraTarget;
    public float cameraSpeed;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position - new Vector3(0, 0, 5), cameraSpeed * Time.deltaTime);
    }
}
