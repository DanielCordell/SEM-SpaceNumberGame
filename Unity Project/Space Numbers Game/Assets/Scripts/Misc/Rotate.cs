using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{


    public float rotationSpeed;

    [RangeAttribute(0.5f, 1.5f)]
    public float randomRotationScale;

    private int direction;

    void Start()
    {
        randomRotationScale = Random.Range(0.5f, 1.5f);
        direction = Random.value < 0.5 ? -1 : 1;
    }
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * randomRotationScale * direction);
    }
}
