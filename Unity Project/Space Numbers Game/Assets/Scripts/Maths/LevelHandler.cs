using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    Level currentLevel;
    public const int MAX_LEVEL = 15;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = GenerateLevel(1);
        Debug.Log(currentLevel.numberOfInputs);
        foreach (Operator op in currentLevel.potentialOperators) { Debug.Log(op); }
        foreach (int[] range in currentLevel.numberRanges) { foreach (int num in range) { Debug.Log(num); } }
        Debug.Log(currentLevel.numberRanges);
        Debug.Log(currentLevel.levelNum);
    }

    Level GenerateLevel(int level)
    {
        switch (level) {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 14:
            case 15: return new Level(1, new int[][] { new []{ 1, 2, 3 }, new []{ 4, 5, 6 } }, new Operator[] { Operator.Add });
        }
        return null;
    }



    // Update is called once per frame
    void Update()
    {
    }
}
