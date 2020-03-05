using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{
    // Count how many question does the Player get right in total
    public int ScoreValue;

    public int CurrentLevelQuestionNum;

    public Text Score;

    public int Level;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseData();
    }

    public void InitialiseData()
    {

        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        ScoreValue = 0;
        UpdateScore();
    }

    public void AddRightNum()
    {
        ScoreValue++;
        Debug.Log("Current Level Right Num: " + ScoreValue);
        UpdateScore();
    }

    public void UpdateScore()
    {
        Score.text = "Score: " + ScoreValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
