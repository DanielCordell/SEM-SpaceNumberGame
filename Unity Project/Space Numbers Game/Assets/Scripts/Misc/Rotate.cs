using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed;
    [RangeAttribute(0.5f, 1.5f)]
    public float randomRotationScale;
    // Update is called once per frame

    void Start()
    {
        randomRotationScale = Random.Range(0.5f, 1.5f);
    }
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * randomRotationScale);
    }
}
