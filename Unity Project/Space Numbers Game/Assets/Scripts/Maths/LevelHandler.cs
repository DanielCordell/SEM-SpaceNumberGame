using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    Level currentLevel;
    public const int MAX_LEVEL = 15;

    public GameObject asteroid;
    public int extraAsteroids;

    // Start is called before the first frame update
    void Start()
    {
        // Current demo code to generate a level
        currentLevel = GenerateLevel(1);
        SetupLevel(ref currentLevel);
    }

    void SetupLevel(ref Level level)
    {
        if (level == null) throw new ArgumentNullException("Level is Null!");
        int numberOfAsteroids = level.GetNumberOfGaps() + extraAsteroids;
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            GameObject a = Instantiate(asteroid);
            int? number = (i < level.numberRanges.Length - 1) ? level.questionNumbers[i] : null;
            a.transform.Find("Canvas/Text").GetComponent<UnityEngine.UI.Text>().text = number.ToString();
        }
    }

    Level GenerateLevel(int level)
    {
        switch (level)
        {
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
            case 15: return new Level(1, new List<int>[] { new List<int> { 1, 2, 3 }, new List<int> { 4, 5, 6 } }, new Operator[] { Operator.Add, Operator.Equals }, new List<bool> { true, false, true });
        }
        return null;
    }



    // Update is called once per frame
    void Update()
    {
    }
}
