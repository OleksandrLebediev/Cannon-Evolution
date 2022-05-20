using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArithmeticOperatorsHandler 
{
    public int Calculation(ArithmeticOperator arithmeticOperator, int a, int b)
    {
        switch (arithmeticOperator)
        {
            case ArithmeticOperator.Add:
                return a + b;
            case ArithmeticOperator.Sub:
                return a - b;
            case ArithmeticOperator.Mul:
                return a * b;
            case ArithmeticOperator.Div:
                return a / b;
            default:
                throw new ArgumentOutOfRangeException(nameof(arithmeticOperator), arithmeticOperator, null);
        }
    }

    public string CalculationDisplay(ArithmeticOperator arithmeticOperator, int a)
    {
        switch (arithmeticOperator)
        {
            case ArithmeticOperator.Add:
                return $"+{a}";
            case ArithmeticOperator.Sub:
                return $"-{a}";
            case ArithmeticOperator.Mul:
                return $"x{a}";
            case ArithmeticOperator.Div:
                return $"÷{a}";
            default:
                throw new ArgumentOutOfRangeException(nameof(arithmeticOperator), arithmeticOperator, null);
        }
    }
}

public enum ArithmeticOperator
{
    Add, 
    Sub,
    Mul,
    Div
}
