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
    List<int> potentialNumbers;
    List<Operator> operators;
    int numbers;
    int questions;
    int blanks;

    LevelInfo levelInfo;
    PlayButtonHandler playButtonHandler;

    void Start()
    {
        levelInfo = gameObject.transform.Find("/LevelSelectCanvas/InfoWindow").GetComponentInChildren<LevelInfo>();

        // levelNumber = 1;
        // levelDifficulty = Difficulty.Easy;
        // potentialNumbers = new List<int>{1,2,3,4,5};
        // operators = new List<Operator>{ Operator.Add };
    }

    void Update()
    {
        
    }

    public void SetUpButton(int level, Difficulty difficulty, List<int> range, List<Operator> ops, int noQuestions, int noNumbers, int noBlanks) {
        levelNumber = level;
        levelDifficulty = difficulty;
        potentialNumbers = range;
        operators = ops;
        questions = noQuestions;
        numbers = noNumbers;
        blanks = noBlanks;

        gameObject.GetComponentInChildren<Text>().text = level.ToString();
        var button = gameObject.GetComponent<Button>();
        ColorBlock colors = button.colors;
        colors.normalColor = difficulty.GetColour();
        button.colors = colors;
        var image = gameObject.GetComponent<Image>();
    }

    public void SetLevelInfo() {
        levelInfo.UpdateInfo(levelNumber, levelDifficulty, potentialNumbers, operators, questions, blanks, numbers);
    }
}

public class NumberRange 
{
    public int RangeFloor { get; set; }
    public int RangeCeiling { get; set; }
}
