using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    int levelNo;
    Difficulty difficulty;
    List<int>  numbers;
    List<Operator> operators;
    int noQuestions;
    int noNumbers;
    int noBlanks;
    bool buttonEnabled;

    // Start is called before the first frame update
    void Start()
    {
        buttonEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedLevel(int level, Difficulty levelDifficulty, List<int> levelNumbers, List<Operator> levelOperators, int questions, int questionNumbers, int questionBlanks)
    {
        levelNo = level;
        difficulty = levelDifficulty;
        numbers = levelNumbers;
        operators = levelOperators;
        noQuestions = questions;
        noNumbers = questionNumbers;
        noBlanks = questionBlanks;

        buttonEnabled = true;
    }

    public void PlayLevel()
    {
        if (buttonEnabled == false)
            return;
    
        Debug.Log("Now playing: Level " + levelNo.ToString());

        //Pass all stuff to the level and do that stuff here
        CurrentLevel.LevelNo = levelNo;
        CurrentLevel.Difficulty = difficulty;
        CurrentLevel.Numbers = numbers;
        CurrentLevel.Operators = operators;
        CurrentLevel.NoNumbers = noNumbers;
        CurrentLevel.NoQuestions = noQuestions;
        CurrentLevel.NoBlanks = noBlanks;
        SceneManager.LoadScene("SpaceScene");
    }
}
