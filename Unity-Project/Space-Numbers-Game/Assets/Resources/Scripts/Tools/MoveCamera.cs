using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float smoothTime;
    Vector3 moveToPosition;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        moveToPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocity, smoothTime);
    }

    public void SetMoveTo(Vector3 position)
    {
        position.z = moveToPosition.z; // Don't update Z positions;
        moveToPosition = position;
    }
}
