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

    // Start is called before the first frame update
    void Start()
    {
        // levelHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<LevelHandler>();
        // var visible = levelHandler.GetVisible();
        QuestionString = "1+2=4";
        var visible = new List<bool>{true, false, false};
        SplitQuestion(QuestionString, visible);
        PositionQuestionComponents();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SplitQuestion(string question, List<bool> blanks)
    {
        var regex = "(?<=[-+*/=])|(?=[-+*/=])";
        var questionList = Regex.Split(question, regex).ToList();

        Items = questionList.Select((value, index) => {      
            GameObject obj;

            if (Regex.IsMatch(value ,"[-+*/=]"))
            {
                Debug.Log("Creating a symbol with value: "+ value);
                obj = Instantiate(symbolPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                Operator op = value.ToString().ToOperator();
                obj.GetComponent<Symbol>().SetValue(op);
                obj.transform.parent = gameObject.transform;
            } 
            else if (blanks[index/2])
            {
                Debug.Log("Creating a blank with value: "+ value);
                obj = Instantiate(blankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                obj.transform.parent = gameObject.transform;
            }
            else
            {
                Debug.Log("Creating a number with value: "+ value);
                obj = Instantiate(numberPrefab);
                obj.GetComponent<Number>().SetValue(int.Parse(value));
                obj.transform.parent = gameObject.transform;
            }

            return obj;
        }).ToList();
    }

    void PositionQuestionComponents()
    {
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();
        
        int i = 0;
        foreach (var child in children)
        {
            float horizontalOffset = -600 + (10 * i);
            child.position = new Vector3(horizontalOffset, -850, 0);
            i++;
        }
    }

    List<int> extractNumbersFromQuestion(string question) {
        var regex = @"[\+\*\/\-=]";
        return Regex.Split(question, regex).Select(int.Parse).ToList();
    }

    public void FillBlank()
    {

    }
}