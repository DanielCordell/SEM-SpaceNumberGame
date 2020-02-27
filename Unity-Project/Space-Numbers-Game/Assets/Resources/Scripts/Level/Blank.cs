using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blank : MonoBehaviour
{
    int? value;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
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

    public bool IsEmpty()
    {
        return value == null;
    }

    public void ClearValue()
    {
        value = null;
    }
}
