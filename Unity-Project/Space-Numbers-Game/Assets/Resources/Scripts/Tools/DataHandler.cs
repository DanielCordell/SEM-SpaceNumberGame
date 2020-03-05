using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    // Count how many question does the Player get right in current level
    public int CurrentLevelRightNum;
    // Count how many question does the Player get right in total
    public int GloablRightNum;
    

    // Start is called before the first frame update
    void Start()
    {
        InitialiseData();
    }

    public void InitialiseData()
    {
        CurrentLevelRightNum = 0;
        GloablRightNum = 0;
    }

    public void UpdateData()
    {
        // When player jump into next level update data
        GloablRightNum += CurrentLevelRightNum;
        CurrentLevelRightNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
