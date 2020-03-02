using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;

public class Question : MonoBehaviour
{
    // LevelHandler levelHandler;
    public List<GameObject> Items;
    public int Answer;
    public string QuestionString;
    public GameObject blankPrefab;
    public GameObject numberPrefab;
    public GameObject symbolPrefab;

    public float gapBetweenItems;

    // Start is called before the first frame update
    void Start()
    {
        // levelHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<LevelHandler>();
        // var visible = levelHandler.GetVisible();
        // QuestionString = "1+2+6=9";
        // var visible = new List<bool>{false, true, false, true};
        // SplitQuestion(QuestionString, visible);
        // PositionQuestionComponents();
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
                obj = Instantiate(symbolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Operator op = value.ToString().ToOperator();
                obj.GetComponent<Symbol>().SetValue(op);
            } 
            else if (!visible[index/2])
            {
                obj = Instantiate(blankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                obj = Instantiate(numberPrefab);
                obj.GetComponent<Number>().SetValue(int.Parse(value));
            }
            obj.transform.SetParent(gameObject.transform, false);

            return obj;
        }).ToList();
    }

    void PositionQuestionComponents()
    {
        // Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        
        int i = 0;
        foreach (Transform child in gameObject.transform)
        {
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

        Debug.Log(statement);
        return statement;
    }

    public int? GetAnswer() {
        var item = Items.Last();

        var blank = item.gameObject.GetComponentInChildren<Blank>();
        if (blank != null)
        {
            return blank.GetValue();
        }

        var number = item.gameObject.GetComponentInChildren<Number>();
        if (number != null)
        {
            return number.GetValue();
        }
        
        Debug.Log("This shouldn't happen, last item is not a blank or a number!");
        return null;
    }
}