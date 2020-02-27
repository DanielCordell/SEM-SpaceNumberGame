﻿using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public float speed;

    PathCreator pathCreator;
    float dstTravelled;

    public GameObject asteroidTarget;

    // Start is called before the first frame update
    void Start()
    {
        dstTravelled = 0;
        pathCreator = GetComponentInParent<PathCreator>();
        Debug.Log(pathCreator);
    }

    // Update is called once per frame
    void Update()
    {
        dstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled);
        Vector3 rotation = (pathCreator.path.GetRotationAtDistance(dstTravelled) * Quaternion.Euler(90, 0, 0)).eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(0, -180, -rotation.x));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Test");
        if (collision.gameObject == asteroidTarget)
        {
            Destroy(collision.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
