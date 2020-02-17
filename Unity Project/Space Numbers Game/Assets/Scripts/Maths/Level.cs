using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Level
{

    // The operators that will be used in this question, in the order they will be used in.
    public readonly Operator[] operatorsUsed;

    // Ranges of generated numbers for each place in the question.
    public List<int>[] numberRanges;

    // The numbers chosen to appear in the question, from numberRanges.
    public int?[] questionNumbers;

    // The level number, for printing.
    public int levelNum;

    // The visbility of each number in the equation.
    public List<bool> visible;

    // Gaps are stored as false, count the number of falses.
    public int GetNumberOfGaps()
    {
        return visible.Count(x => !x);
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

        questionNumbers = new int?[visible.Count];
        Random rand = new Random();

        for (int i = 0; i < numberRanges.Length; i++)
        {
            int index = rand.Next(numberRanges[i].Count);
            int chosen = numberRanges[i][index];
            questionNumbers[i] = chosen;

            foreach (List<int> range in numberRanges)
            {
                range.Remove(chosen);
            }
            questionNumbers[questionNumbers.Length - 1] = null;
        }

    }
}
