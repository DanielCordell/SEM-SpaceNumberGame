using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipHandler : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 lerpStartPosition;
    Vector3 moveToPosition;

    bool overrideWithFixedMovement;
    bool fixedMovementFinished;

    public float timeForMove;
    float timeMoved;

    public float movementMultiplier;

    Vector3 velocityForFixedMovement = Vector3.zero;

    bool isQuit = false;

    void Start()
    {
        startPosition = transform.position;
        lerpStartPosition = startPosition;
        moveToPosition = GetNewTargetPosition();
        timeMoved = 0;

        overrideWithFixedMovement = false;
        fixedMovementFinished = false;

        isQuit = false;
    }

    void Update()
    {
        if (fixedMovementFinished)
        {
            if (isQuit)
            {
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
            }
        }
        if (overrideWithFixedMovement)
        {
            transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocityForFixedMovement, 0.5f);
            if (Vector3.Distance(moveToPosition, transform.position) < 2) fixedMovementFinished = true;
        }
        else
        {
            timeMoved += Time.deltaTime;
            bool hasFinishedMove = timeMoved >= timeForMove;
            transform.position = Vector3.Lerp(lerpStartPosition, moveToPosition, hasFinishedMove ? 1 : timeMoved / timeForMove);
            if (hasFinishedMove)
            {
                timeMoved = 0;
                moveToPosition = GetNewTargetPosition();
                lerpStartPosition = transform.position;
            }
        }
    }

    public Vector3 GetNewTargetPosition()
    {
        return startPosition + (Vector3)(Random.insideUnitCircle.normalized * movementMultiplier);
    }

    public void SetTargetObject(GameObject obj)
    {
        if (obj.name == "QuitObject") isQuit = true;
        moveToPosition = obj.transform.position;
        overrideWithFixedMovement = true;
    }
}
