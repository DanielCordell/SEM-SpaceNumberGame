using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfo : MonoBehaviour
{
    //PlayButton playButton
    Text level;
    Text difficulty;
    Text numbers;
    Text operators;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInfo(int levelNumber, Difficulty levelDifficulty, NumberRange numberRange, List<Operator> levelOperators)
    {
        Debug.Log("Updating Info Pane");
        level.text = "Level " + levelNumber.ToString();
        difficulty.text = levelDifficulty.ToString();
        numbers.text = "Numbers " + numberRange.RangeFloor.ToString() + " to " + numberRange.RangeCeiling.ToString();
        operators.text = String.Join("   ", levelOperators.Select(o => {
            return o.ToOpString();
        }));

        //Update play button
    }
}
