using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{ 
    LevelsDTO levels;
    public GameObject Button;
    public int GapBetweenItems;
    public int Rows;
    public int Columns;

    List<GameObject> buttons;

    // Start is called before the first frame update
    void Start()
    {

        loadLevels();

        int c = 0;
        int r = 0;
        foreach (var level in levels.Levels)
        {
            var button = createLevelButton(level);
            positionButton(button, c, r);

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

    void loadLevels()
    {
        var jsonFile = Resources.Load<TextAsset>("Config/Levels");
        Debug.Log(jsonFile.ToString());
        levels = JsonUtility.FromJson<LevelsDTO>(jsonFile.ToString());
        Debug.Log(levels.Levels.Count.ToString());
    }

    void positionButton(GameObject button, int column, int row)
    {   
        float horizontalOffset = -120 + (GapBetweenItems * column);
        float verticalOffset = 100 - (GapBetweenItems * row);
        button.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, -10);
    }

    GameObject createLevelButton(LevelDTO level)
    {
        var button = Instantiate(Button);
        button.transform.SetParent(gameObject.transform, false);

        var handler = button.gameObject.GetComponentInChildren<LevelSelectButtonHandler>();
        handler.SetUpButton(level.LevelNo, level.Difficulty, level.Numbers, level.Operators);

        return button;
    }
}

[Serializable]
public class LevelDTO
{
    public int LevelNo;
    public Difficulty Difficulty;
    public List<int> Numbers;
    public List<Operator> Operators;
    public int NoQuestions;
    public int NoNumbers;
    public int NoBlanks;
}

[Serializable]
public class LevelsDTO
{
    public List<LevelDTO> Levels;
}
