using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    int value;
    Text text;

    void Start()
    {
        text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        text.text = value.ToString();
    }

    public void SetValue(int input) 
    {
        value = input;
    }

    public int GetValue()
    {
        return value;
    }
}