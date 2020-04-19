using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cameraTarget;
    public float cameraSpeed;
    public float cameraDistance;

   
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, cameraTarget.transform.position - new Vector3(0, 0, cameraDistance), cameraSpeed * Time.deltaTime);
    }
}
