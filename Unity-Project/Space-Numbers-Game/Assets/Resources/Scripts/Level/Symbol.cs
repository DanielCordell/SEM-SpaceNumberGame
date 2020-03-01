using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    Operator value;
    Text text;

    void Start()
    {
        text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        text.text = value.ToOpString();
    }

    public void SetValue(Operator op)
    {
        Debug.Log(op);
        value = op;
    }

    public Operator GetValue()
    {
        return value;
    }
}