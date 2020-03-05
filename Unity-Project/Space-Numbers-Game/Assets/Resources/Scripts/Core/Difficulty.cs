using System;
using UnityEngine;

public enum Difficulty
{
    Easy, 
    Medium,
    Hard,
    Extreme
}

public static class DifficultyExtensions
{
    public static Color GetColour(this Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return new Color(125, 227, 132, 255);
            case Difficulty.Medium: return new Color(247, 172, 52, 255);
            case Difficulty.Hard: return new Color(237, 64, 12, 255);
            case Difficulty.Extreme: return new Color(169, 11, 222, 255);
            default: throw new ArgumentException("Invalid difficulty: " + difficulty.ToString());
        }
    } 

    public static string ToString(this Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return "Easy";
            case Difficulty.Medium: return "Medium";
            case Difficulty.Hard: return "Hard";
            case Difficulty.Extreme: return "Extreme";
            default: throw new ArgumentException("Invalid difficulty: " + difficulty.ToString());
        }
    }

    public static Difficulty ToDifficulty(this string input)
    {
        switch (input)
        {
            case "Easy": return Difficulty.Easy;
            case "Medium": return Difficulty.Medium;
            case "Hard": return Difficulty.Hard;
            case "Extreme": return Difficulty.Extreme;
            default: throw new ArgumentException("Invalid difficulty: " + input);
        }
    }
}