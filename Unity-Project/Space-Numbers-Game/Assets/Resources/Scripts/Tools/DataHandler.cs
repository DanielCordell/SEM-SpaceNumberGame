using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{
    // Count how many question does the Player get right in current level
    public int CurrentLevelRightNum;
    // Count how many question does the Player get right in total
    public int GloablRightNum;

    public int CurrentLevelQuestionNum;

    public Text score;

    


    // Start is called before the first frame update
    void Start()
    {
        InitialiseData();
    }

    public void InitialiseData()
    {
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        CurrentLevelRightNum = 0;
        GloablRightNum = 0;
    }

    public void ResetCurrentLevelRightNum()
    {
        // When player jump into next level update data
        GloablRightNum += CurrentLevelRightNum;
        CurrentLevelRightNum = 0;
    }

    public void AddRightNum()
    {
        CurrentLevelRightNum++;
        GloablRightNum++;
        Debug.Log("Current Level Right Num: " + CurrentLevelRightNum);
        UpdateScore();
    }

    public void UpdateScore()
    {
        score.text = GloablRightNum.ToString();
    }


    public void UpdateCurrentLevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
