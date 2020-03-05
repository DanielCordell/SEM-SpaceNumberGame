using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    public PlayButtonHandler PlayButtonHandler;
    Text level;
    Text difficulty;
    Text numbers;
    Text operators;
    Text blanks;
    TextAsset noNumbers;

    // Start is called before the first frame update
    void Start()
    {
        GameObject levelInfo = gameObject.transform.Find("LevelInfo").gameObject;

        level = levelInfo.transform.Find("LevelNumber").gameObject.GetComponent<Text>();
        level.text = "";
        difficulty = levelInfo.transform.Find("Difficulty").gameObject.GetComponent<Text>();
        difficulty.text = "";
        numbers = levelInfo.transform.Find("Numbers").gameObject.GetComponent<Text>();
        numbers.text = "";
        operators = levelInfo.transform.Find("Operators").gameObject.GetComponent<Text>();
        operators.text = "";
        
        SetBlanksText("");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetBlanksText(string text)
    {
        var isActive = text == "" ? false : true;

        Transform blanksObj = gameObject.transform.Find("LevelInfo").gameObject.transform.Find("Blanks").gameObject.transform;
        blanks = blanksObj.Find("BlanksText").gameObject.GetComponent<Text>();
        blanks.text = text;
        blanksObj.Find("AsteroidBlankSprite").gameObject.SetActive(isActive);
    }

    public void UpdateInfo(int levelNumber, Difficulty levelDifficulty, List<int> potentialNumbers, List<Operator> levelOperators, int questions, int questionBlanks, int questionNumbers)
    {
        Debug.Log("Updating Info Pane");

        level.text = "Level " + levelNumber;

        difficulty.text = levelDifficulty.ToString();

        numbers.text = "Numbers " + potentialNumbers.Min() + " to " + potentialNumbers.Max();
        
        operators.text = String.Join("   ", levelOperators.Select(o => {
            return o.ToOpString();
        }));

        SetBlanksText(questionBlanks + "x");

        //Update play button

        PlayButtonHandler.SetSelectedLevel(levelNumber, levelDifficulty, potentialNumbers, levelOperators, questions, questionBlanks, questionNumbers);
    }
}
