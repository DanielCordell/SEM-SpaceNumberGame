using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEditor;

public class Level
{

    // The operators that will be used in this question, in the order they will be used in.
    public readonly Operator[] operatorsUsed;

    // Ranges of generated numbers for each place in the question.
    public List<int>[] numberRanges;

    // The numbers chosen to appear in the question, from numberRanges.
    public int[] questionNumbers;

    // The level number, for printing.
    public int levelNum;

    // The visbility of each number in the equation.
    public List<bool> visible;

    // The maths statement string this question is based on, with everything filled in, for debugging. (e.g. "5+4=9")
    public String statementString;

    // Gaps are stored as false, count the number of falses.
    public int GetNumberOfGaps()
    {
        return visible.Count(x => !x);
    }
    
    // This function returns the values that are in the 'gaps' in the equation. Used for asteroid generation.
    public int[] GetValuesOfGaps()
    {
        return questionNumbers.Where((val, i) => !visible[i]).ToArray();
    }

    public Level(int levelNum, List<int>[] numberRanges, Operator[] operators, List<bool> visible)
    {
        // Ensure we were passed in valid params
        if (numberRanges.Length + 1 != visible.Count)
            throw new ArgumentException("numberRanges Length + 1 != visible Length!\n numberRanges: " + numberRanges.Length + " visibile Length: " + visible.Count);
        if (numberRanges.Length != operators.Length)
            throw new ArgumentException("numberRanges Length != operators Length!\n numberRanges: " + numberRanges.Length + " operators Length: " + operators.Length);

        this.levelNum = levelNum;
        this.numberRanges = numberRanges;
        this.operatorsUsed = operators;
        this.visible = visible;

        // Array of question numbers, with last value (answer) as null (we still need to calculate  it)
        int?[] questionNumbersNoAnswer = new int?[visible.Count];
        System.Random rand = new System.Random();

        for (int i = 0; i < numberRanges.Length; i++)
        {
            // Choose a random number from the passed in range for that specific input space.
            int index = rand.Next(numberRanges[i].Count);
            int chosen = numberRanges[i][index];
            questionNumbersNoAnswer[i] = chosen;

            // Ensure we don't get any duplicates by removing the number from the range!
            foreach (List<int> range in numberRanges)
            {
                range.Remove(chosen);
            }
        }

        // Generate expression from chosen numbers
        String expressionString = GenerateExpressionString(questionNumbersNoAnswer);
        Debug.Log("Generated Expression: " + expressionString);

        // Calculate result of expression
        int result = 0; 
        ExpressionEvaluator.Evaluate<int>(expressionString, out result);
        Debug.Log("Answer: " + result);

        // Save expression result into questionNumbersNoAnswer, then copy to local questionNumbers and remove nullable (as we have a value for it now!).
        questionNumbersNoAnswer[questionNumbersNoAnswer.Length - 1] = result;
        questionNumbers = questionNumbersNoAnswer.Select(it => it.Value).ToArray();

        // Debug
        statementString = expressionString + "=" + result;
        LogDebugInfo();
    }

    // From our picked numbers, generate the expression string. (e.g. "5+1/3*4")
    private String GenerateExpressionString(int?[] questionNumbersNoAnswer)
    {
        String expression = "";
        for (int i = 0; i < operatorsUsed.Length; i++)
        {
            expression += questionNumbersNoAnswer[i];
            if (operatorsUsed[i] == Operator.Equals) return expression; // Don't add the equals to the 'expression', as we want an expression not a statement.
            expression += operatorsUsed[i].ToOpString();
        }
        throw new ArgumentException("Somehow generated a question with no Equals sign!");
    }

    public void LogDebugInfo()
    {
        Debug.Log("Level: " + levelNum);
        Debug.Log("Question: " + statementString);
    }
}
