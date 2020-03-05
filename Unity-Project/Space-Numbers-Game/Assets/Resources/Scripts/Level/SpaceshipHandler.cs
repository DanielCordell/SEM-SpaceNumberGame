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

    [Range(1, 5)]
    public float TimeForMove;
    float timeMoved;

    // Scales size of random position in unit circle for larger movement
    [Range(0, 2)]
    public float RandomPositionMovementMultiplier;

    Vector3 velocityForFixedMovement = Vector3.zero;

    bool isQuit = false;

    void Start()
    {
        GameObject spawnObject = GameObject.FindGameObjectWithTag("SpaceshipSpawnPos");
        startPosition = spawnObject.transform.position;
        lerpStartPosition = startPosition;
        moveToPosition = GetNewTargetPosition();
        timeMoved = 0;

        overrideWithFixedMovement = false;
        fixedMovementFinished = false;

        isQuit = false;


        SetTargetObject(spawnObject);
        transform.position += new Vector3(0, -10, 0);
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
            else
            {
                overrideWithFixedMovement = false;
                fixedMovementFinished = false;
                ResetMove();
            }
        }
        if (overrideWithFixedMovement)
        {
            transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocityForFixedMovement, 0.5f);
            /// We want it to shoot off quickly so the distance needs to be far away, but we don't want the user to wait too long on quit. So if quit it actualy checks BEFORE the end and quits when the ship is 2 units away.
            float requiredDistance = isQuit ? 2.0f : 0.01f; 
            if (Vector3.Distance(moveToPosition, transform.position) < requiredDistance) fixedMovementFinished = true;
        }
        else
        {
            timeMoved += Time.deltaTime;
            bool hasFinishedMove = timeMoved >= TimeForMove;
            transform.position = Vector3.Lerp(lerpStartPosition, moveToPosition, hasFinishedMove ? 1 : timeMoved / TimeForMove);
            if (hasFinishedMove)
            {
                ResetMove();
            }
        }
    }

    private void ResetMove()
    {
        timeMoved = 0;
        moveToPosition = GetNewTargetPosition();
        lerpStartPosition = transform.position;
    }

    public Vector3 GetNewTargetPosition()
    {
        return startPosition + (Vector3)(Random.insideUnitCircle.normalized * RandomPositionMovementMultiplier);
    }

    public void SetTargetObject(GameObject obj)
    {
        if (obj.name == "QuitObject") isQuit = true;
        moveToPosition = obj.transform.position;
        overrideWithFixedMovement = true;
    }
}
