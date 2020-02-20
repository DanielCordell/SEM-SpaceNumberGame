using System;

public enum Operator
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Equals
}
static class OperatorExtension
{
    public static string ToOpString(this Operator op)
    {
        switch (op)
        {
            case Operator.Add:      return "+";
            case Operator.Divide:   return "/";
            case Operator.Equals:   return "=";
            case Operator.Multiply: return "*";
            case Operator.Subtract: return "-";
            default: throw new ArgumentException("Bad Operator: " + op.ToString());
        }
    }
}