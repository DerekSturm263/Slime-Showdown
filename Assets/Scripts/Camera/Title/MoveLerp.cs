using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLerp : MonoBehaviour
{
    public Vector3 positionNew;

    private void Start()
    {
        positionNew = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, positionNew, Time.deltaTime * 0.25f);

        if (Vector3.Distance(transform.position, positionNew) < 1f)
            GetNewPosition();
    }

    private void GetNewPosition()
    {
        int posX = Random.Range(-5, 5);
        int posY = Random.Range(-3, 3);

        positionNew = new Vector3(posX, posY, transform.position.z);
    }
}
