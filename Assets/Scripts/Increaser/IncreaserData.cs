using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IncreaserData
{
    [SerializeField] private int _numberMagnification;
    [SerializeField] private ArithmeticOperator _arithmeticOperator;
    
    
    public int NumberMagnification => _numberMagnification;
    public ArithmeticOperator ArithmeticOperator => _arithmeticOperator;
}
