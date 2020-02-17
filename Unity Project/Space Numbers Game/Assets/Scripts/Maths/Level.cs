using System.Collections;
using System.Collections.Generic;
using System;

public class Level
{

    // The operators that will be used in this question, in the order they will be used in.
    public readonly Operator[] operatorsUsed;

    // Ranges of generated numbers for each place in the question.
    public int[][] numberRanges;

    // The numbers chosen to appear in the question, from numberRanges.
    public int[] questionNumbers;

    // The level number, for printing.
    public int levelNum;

    // The visbility of each number in the equation.
    public bool[] visible;

    public Level(int levelNum, int[][] numberRanges, Operator[] operators, bool[] visible)
    {
        // Ensure we were passed in valid params
        if (numberRanges.Length != visible.Length)
            throw new ArgumentException("numberRanges.Length != visible.Length!\n numberRanges: " + numberRanges.Length + " visibile.Length: " + visible.Length);
        if (numberRanges.Length != operators.Length + 1)
            throw new ArgumentException("numberRanges.Length != operators.Length + 1!\n numberRanges: " + numberRanges.Length + " operators.Length + 1: " + (operators.Length + 1));

        this.levelNum = levelNum;
        this.numberRanges = numberRanges;
        this.operatorsUsed = operators;
        this.visible = visible;
    }
}
