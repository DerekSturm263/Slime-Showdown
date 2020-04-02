using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLerp : MonoBehaviour
{
    public float lerpSpeed;
    public float xMinMax;
    public float yMinMax;

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
        float posX = Random.Range(-xMinMax, xMinMax);
        float posY = Random.Range(-yMinMax, yMinMax);

        positionNew = new Vector3(posX, posY, transform.position.z);
    }
}
