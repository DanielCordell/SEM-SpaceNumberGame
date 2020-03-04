using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButtonHandler : MonoBehaviour
{
    int levelNumber;
    Difficulty levelDifficulty;
    NumberRange numberRange;
    List<Operator> operators;

    public LevelInfo LevelInfo;

    // Start is called before the first frame update
    void Start()
    {
        levelNumber = 1;
        levelDifficulty = Difficulty.Easy;
        numberRange = new NumberRange{ RangeFloor = 1, RangeCeiling = 10};
        operators = new List<Operator>{ Operator.Add };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelInfo() {
        //Update level info pane with info
        LevelInfo.UpdateInfo(levelNumber, levelDifficulty, numberRange, operators);
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
