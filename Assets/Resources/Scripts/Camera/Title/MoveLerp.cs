using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLerp : MonoBehaviour
{
    public float lerpSpeed;

    public float xMin, xMax;
    public float yMin, yMax;

    private Vector3 positionNew;

    private void Start()
    {
        positionNew = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, positionNew, Time.deltaTime * lerpSpeed);

        if (Vector3.Distance(transform.position, positionNew) < 1f)
            GetNewPosition();
    }

    private void GetNewPosition()
    {
        float posX = Random.Range(xMin, xMax);
        float posY = Random.Range(yMin, yMax);

        positionNew = new Vector3(posX, posY, transform.position.z);
    }
}
