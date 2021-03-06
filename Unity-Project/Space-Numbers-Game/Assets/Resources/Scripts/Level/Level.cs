﻿using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using NCalc;
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

    // Find where the division signs are
    public static int[] FindIndexOfDivisionSign(Operator[] operatorsUsed)
    {
        return operatorsUsed.Select((op, i) => op == Operator.Divide ? i : -1).Where(i => i != -1).ToArray();
    }

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
        if (visible.Count(it => !it) == 0)
            throw new ArgumentException("visible needs at least one false value, otherwise the question doesn't make sense!");
        if (operators[operators.Length - 1] != Operator.Equals)
            throw new ArgumentException("Last operator must be equals, instead found " + operators[operators.Length - 1].ToOpString());

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

            // Ensure we don't get any (more than twice) duplicates by removing the number from the range, unless that leaves us with nothing in the list!
            // This means numbers can appear twice in an expression, but no more times, unless there's no other number to choose.
            foreach (List<int> range in numberRanges)
            {
                if (range.Count > 1 && questionNumbersNoAnswer.Count(it => it == chosen) > 2) range.Remove(chosen);
            }
        }

        // Generate expression from chosen numbers
        String expressionString = GenerateExpressionString(questionNumbersNoAnswer, ref operatorsUsed);
        Debug.Log("Generated Expression: " + expressionString);

        // Turning division into a multiplication
        if (expressionString.Contains('/'))
        {
            int count = this.operatorsUsed.Count(op => op == Operator.Divide);
            Debug.Log("Number of division signs: " + count);
            int[] indexOfDvisionSigns = FindIndexOfDivisionSign(operatorsUsed);
            for (int i = 0; i < indexOfDvisionSigns.Length; i++)
            {
                // Make sure 0 cannot be denominator
                if (questionNumbersNoAnswer[indexOfDvisionSigns[i] + 1] == 0)
                    questionNumbersNoAnswer[indexOfDvisionSigns[i] + 1] += rand.Next(1, 10);
                // If not divisible, add a random value
                while ((questionNumbersNoAnswer[indexOfDvisionSigns[i]] % questionNumbersNoAnswer[indexOfDvisionSigns[i] + 1]) != 0)
                    questionNumbersNoAnswer[indexOfDvisionSigns[i]] += rand.Next(0, 4);
            }
            expressionString = GenerateExpressionString(questionNumbersNoAnswer, ref operatorsUsed);
            Debug.Log("Regenerated Expression: " + expressionString);
        }

        // Calculate result of expression
        int result = 0;
        try
        {
            result = Convert.ToInt32(new Expression(expressionString).Evaluate());
        }
        catch (DivideByZeroException e)
        {
            throw new DivideByZeroException("When evaluating expression, a division by zero occured!", e);
        } catch(Exception e)
        {
            throw new ArgumentException("An exception occured when trying to pass an expression to ExpressionEvaluator", e);
        }

        // Save expression result into questionNumbersNoAnswer, then copy to local questionNumbers and remove nullable (as we have a value for it now!).
        questionNumbersNoAnswer[questionNumbersNoAnswer.Length - 1] = result;
        questionNumbers = questionNumbersNoAnswer.Select(it => it.Value).ToArray();

        // Debug
        statementString = expressionString + "=" + result;
        LogDebugInfo();
    }

    // From our picked numbers, generate the expression string. (e.g. "5+1/3*4")
    // Make any necessary changes to the operatorsUsed to fix this.
    public static String GenerateExpressionString(int?[] questionNumbersNoAnswer, ref Operator[] operatorsUsed)
    {
        String expression = "";
        System.Random rand = new System.Random();
        for (int i = 0; i < operatorsUsed.Length; i++)
        {
            expression += questionNumbersNoAnswer[i];
            Debug.Log("questionNumbersNoAnswer: " + questionNumbersNoAnswer[i]);
            if (operatorsUsed[i] == Operator.Equals) return expression; // Don't add the equals to the 'expression', as we want an expression not a statement.
            // Make sure no consecutive division
            // TODO this should be refactored out of here!
            if ((operatorsUsed[i] == Operator.Divide) && (operatorsUsed[i + 1] == Operator.Divide))
            {
                operatorsUsed[i + 1] = (Operator) rand.Next(0, 3);
            }
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
