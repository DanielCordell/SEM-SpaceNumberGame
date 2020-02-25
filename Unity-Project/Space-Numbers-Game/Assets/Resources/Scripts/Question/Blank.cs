using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blank : Item
{
    int? value;

    // Start is called before the first frame update
    void Start()
    {
        value = 69;
        text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        text.text = value == null ? "" : value.ToString();
    }

    public int? GetValue() 
    {
        return value;
    }

    public void SetValue(int input) 
    {
        value = input;
    }

    public void ClearValue() 
    {
        value = null;
    }
}
