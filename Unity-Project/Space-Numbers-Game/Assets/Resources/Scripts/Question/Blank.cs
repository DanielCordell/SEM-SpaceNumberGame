using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blank : MonoBehaviour
{
    int? Value;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.transform.Find("Text").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {   
        text.text = Value == null ? "" : Value.ToString();
    }

    public int? GetValue()
    {
        return Value;
    }

    public void SetValue(int input)
    {
        Value = input;
    }

    public void ClearValue()
    {
        Value = null;
    }
}
