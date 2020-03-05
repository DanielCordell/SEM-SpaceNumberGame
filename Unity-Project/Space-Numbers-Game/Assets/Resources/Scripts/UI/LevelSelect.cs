using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{ 
    LevelsDTO levels;
    public GameObject Button;
    public int GapBetweenItems;
    public int Rows;
    public int Columns;
    
    public PlayButtonHandler playButton;
    List<GameObject> buttons;

    // Start is called before the first frame update
    void Start()
    {

        LoadLevels();

        int c = 0;
        int r = 0;
        foreach (var level in levels.Levels)
        {
            var button = CreateLevelButton(level);
            PositionButton(button, c, r);

            c++;
            if (c >= Columns)
            {
                r++;
                c = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevels()
    {
        var jsonFile = Resources.Load<TextAsset>("Config/Levels");
        Debug.Log(jsonFile.ToString());
        levels = JsonUtility.FromJson<LevelsDTO>(jsonFile.ToString());
        Debug.Log(levels.Levels.Count.ToString());
    }

    void PositionButton(GameObject button, int column, int row)
    {   
        float horizontalOffset = -120 + (GapBetweenItems * column);
        float verticalOffset = 100 - (GapBetweenItems * row);
        button.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, -10);
    }

    GameObject CreateLevelButton(LevelDTO level)
    {
        var button = Instantiate(Button);
        button.transform.SetParent(gameObject.transform, false);

        var handler = button.gameObject.GetComponentInChildren<LevelSelectButtonHandler>();

        Difficulty difficulty = level.Difficulty.ToDifficulty();
        List<Operator> operators = level.Operators.Select(o => o.ToOperator()).ToList();

        handler.SetUpButton(level.LevelNo, difficulty, level.Numbers, operators, level.NoQuestions, level.NoNumbers, level.NoBlanks);

        return button;
    }
}

[Serializable]
public class LevelDTO
{
    public int LevelNo;
    public string Difficulty;
    public List<int> Numbers;
    public List<string> Operators;
    public int NoQuestions;
    public int NoNumbers;
    public int NoBlanks;
}

[Serializable]
public class LevelsDTO
{
    public List<LevelDTO> Levels;
}
