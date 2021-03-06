﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using System;

public class Question : MonoBehaviour
{
    // LevelHandler levelHandler;
    public List<GameObject> Items;
    public int Answer;
    public string QuestionString;
    public GameObject PrefabBlank;
    public GameObject PrefabNumber;
    public GameObject PrefabSymbol;

    public float gapBetweenItems;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetQuestion(string questionString, List<bool> visible)
    {
        SplitQuestion(questionString, visible);
        PositionQuestionComponents();
    }

    void SplitQuestion(string question, List<bool> visible)
    {
        var regex = "(?<=[-+*/=])|(?=[-+*/=])";
        var questionList = Regex.Split(question, regex).ToList();

        Items = questionList.Select((value, index) => {      
            GameObject obj;

            if (Regex.IsMatch(value ,"[-+*/=]"))
            {
                obj = Instantiate(PrefabSymbol, new Vector3(0, 0, 0), Quaternion.identity);
                Operator op = value.ToString().ToOperator();
                obj.GetComponent<Symbol>().SetValue(op);
            } 
            else if (!visible[index/2])
            {
                obj = Instantiate(PrefabBlank, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                obj = Instantiate(PrefabNumber);
                obj.GetComponent<Number>().SetValue(int.Parse(value));
            }
            obj.transform.SetParent(gameObject.transform, false);

            return obj;
        }).ToList();
    }

    void PositionQuestionComponents()
    {   
        int i = 0;
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject == null && !child.gameObject.activeInHierarchy) continue;
            if (child == gameObject.transform) continue;
            float horizontalOffset = -110 + (gapBetweenItems * i);
            child.localPosition = new Vector3(horizontalOffset, 0, -10);
            i++;
        }
    }

    List<int> extractNumbersFromQuestion(string question) {
        var regex = @"[\+\*\/\-=]";
        return Regex.Split(question, regex).Select(int.Parse).ToList();
    }

    public bool FillBlank(int value)
    {
        var blanks = gameObject.GetComponentsInChildren<Blank>();
        var set = false;

        foreach (Blank blank in blanks)
        {
            if (blank.IsEmpty())
            {
                blank.SetValue(value);
                set = true;
                break;
            }
        }

        return set;
    }

    public void ClearBlank(int value)
    {
        var blanks = gameObject.GetComponentsInChildren<Blank>();

        foreach (Blank blank in blanks)
        {
            if (blank.GetValue() == value)
            {
                blank.ClearValue();
                break;
            }
        }
    }

    public string GetExpression()
    {
        string statement = "";

        foreach (var item in Items)
        {
            var blank = item.gameObject.GetComponentInChildren<Blank>();
            if (blank != null)
            {
                var blankVal = blank.GetValue();
                var blankString =  blankVal == null ? "" : blank.GetValue().ToString();
                statement = statement + blankString;
                continue;
            }

            var number = item.gameObject.GetComponentInChildren<Number>();
            if (number != null)
            {
                statement = statement + number.GetValue().ToString();
                continue;
            }

            var symbol = item.gameObject.GetComponentInChildren<Symbol>();
            if (symbol != null)
            {
                if (symbol.GetValue() == Operator.Equals) break;
                statement = statement + symbol.GetValue().ToOpString();
                continue;
            }
        }

        Debug.Log("Current expression is: " + statement);
        return statement;
    }

    public bool AreAllGapsFilled()
    {
        return Items.Select(it => it.GetComponentInChildren<Blank>()).Where(it => it != null).All(it => it.GetValue() != null);
    }

    public int? GetAnswer() {
        var item = Items.Last();

        int? value = null;

        var blank = item.gameObject.GetComponentInChildren<Blank>();
        if (blank != null)
        {
            value = blank.GetValue();
        }

        var number = item.gameObject.GetComponentInChildren<Number>();
        if (number != null)
        {
            value = number.GetValue();
        }

        // If we haven't got a value at this point, something broke!
        if (value == null)
        {
            Debug.Log("This shouldn't happen, last item is not a blank or a number!");
            return null;
        }

        // Could potentially happen somehow
        if (Items.Count == 1) return value;

        // Flip the sign if the previous value is -1
        var penultimateItem = Items[Items.Count - 2].gameObject.GetComponentInChildren<Symbol>();
        if (penultimateItem != null && penultimateItem.GetValue() == Operator.Subtract)
        {
            value = -value;
        }
        return value;
    }

    public void Clear()
    {
        Items.Clear();
    }
}