using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : Item
{
    private int value;
    void Start()
    {
        value = 12;
        text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        text.text = value.ToString();
    }
    
    public Number(int num)
    {
        value = num;
    }

    public int GetValue()
    {
        return value;
    }
}