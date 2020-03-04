using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonHandler : MonoBehaviour
{
    int levelNumber;
    Difficulty levelDifficulty;
    List<int> numberRange;
    List<Operator> operators;

    LevelInfo levelInfo;

    // Start is called before the first frame update
    void Start()
    {
        levelInfo = gameObject.transform.Find("/LevelSelectCanvas/InfoWindow").GetComponentInChildren<LevelInfo>();
        // levelNumber = 1;
        // levelDifficulty = Difficulty.Easy;
        // numberRange = new List<int>{1,2,3,4,5};
        // operators = new List<Operator>{ Operator.Add };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpButton(int level, Difficulty difficulty, List<int> range, List<Operator> ops) {
        levelNumber = level;
        levelDifficulty = difficulty;
        numberRange = range;
        operators = ops;

        gameObject.GetComponentInChildren<Text>().text = level.ToString();
    }

    public void SetLevelInfo() {
        //Update level info pane with info

        var range = new NumberRange { RangeFloor = numberRange.Min(), RangeCeiling = numberRange.Max()};
        levelInfo.UpdateInfo(levelNumber, levelDifficulty, range, operators);
    }
}

public class NumberRange 
{
    public int RangeFloor { get; set; }
    public int RangeCeiling { get; set; }
}

public enum Difficulty
{
    Easy, 
    Medium,
    Hard,
    Extreme
}

public static class DifficultyExtensions
{
    public static string ToString(this Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return "Easy";
            case Difficulty.Medium: return "Medium";
            case Difficulty.Hard: return "Hard";
            case Difficulty.Extreme: return "Extreme";
            default: throw new ArgumentException("Invalid difficulty: " + difficulty.ToString());
        }
    }
}
