using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ConfigData
{
    public static LevelsDTO LevelData;
    public static int NumberOfLevels;

    static ConfigData()
    {
        var jsonFile = Resources.Load<TextAsset>("Config/Levels");
        Debug.Log(jsonFile.ToString());
        LevelData = JsonUtility.FromJson<LevelsDTO>(jsonFile.ToString());
        Debug.Log(LevelData.Levels.Count.ToString());

        NumberOfLevels = LevelData.Levels.Count;
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
