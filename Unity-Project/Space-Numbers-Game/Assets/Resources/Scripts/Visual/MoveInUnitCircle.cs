using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInUnitCircle : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 lerpStartPosition;
    Vector3 moveToPosition;

    public float timeForMove;
    float timeMoved;

    public float movementMultiplier;

    void Start()
    {
        startPosition = transform.position;
        lerpStartPosition = startPosition;
        moveToPosition = GetNewTargetPosition();
        timeMoved = 0;
    }

    void Update()
    {
        timeMoved += Time.deltaTime;
        bool hasFinishedMove = timeMoved >= timeForMove;
        Debug.Log(timeMoved.ToString() + " " + timeForMove.ToString());
        transform.position = Vector3.Lerp(lerpStartPosition, moveToPosition, hasFinishedMove ? 1 : timeMoved / timeForMove);
        if (hasFinishedMove)
        {
            Debug.Log("HasFinishedMove");
            timeMoved = 0;
            moveToPosition = GetNewTargetPosition();
            lerpStartPosition = transform.position;
        }
    }

    public Vector3 GetNewTargetPosition()
    {
        return startPosition + (Vector3)(Random.insideUnitCircle.normalized * movementMultiplier);
    }
}
