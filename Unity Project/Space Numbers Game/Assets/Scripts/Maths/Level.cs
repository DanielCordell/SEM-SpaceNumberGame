using System.Collections;
using System.Collections.Generic;
using System;

public class Level
{
    public readonly Operator[] potentialOperators;

    public readonly int numberOfInputs;

    public int[][] numberRanges;

    public int levelNum;

    public Level(int levelNum, int[][] numberRanges, Operator[] operators)
    {
        this.levelNum = levelNum;
        this.numberRanges = numberRanges;
        this.numberOfInputs = numberRanges.Length;
        this.potentialOperators = operators;
    }

}
