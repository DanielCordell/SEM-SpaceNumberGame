using System;

public enum Operator
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Equals
}

public static class OperatorExtension
{
    public static string ToOpString(this Operator op)
    {
        switch (op)
        {
            case Operator.Add:      return "+";
            case Operator.Subtract: return "-";
            case Operator.Multiply: return "*";
            case Operator.Divide:   return "/";
            case Operator.Equals:   return "=";
            default: throw new ArgumentException("Bad Operator: " + op.ToString());
        }
    }

    public static Operator ToOperator(this string str)
    {
        switch (str)
        {
            case "+": return Operator.Add;
            case "-": return Operator.Subtract;
            case "*": return Operator.Multiply;
            case "/": return Operator.Divide;
            case "=": return Operator.Equals;
            default: throw new ArgumentException("String \"" + str + "\" not a valid operator.");
        }
    }
}