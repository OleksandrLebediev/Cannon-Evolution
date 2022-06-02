using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class IncreaserDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _numberMagnification;
    [SerializeField] private TMP_Text _partDate;

    
    public void Initialize(IncreaserData data)
    {
        ArithmeticOperatorsHandler arithmetic = new ArithmeticOperatorsHandler();
        _numberMagnification.text = arithmetic.CalculationDisplay(data.ArithmeticOperator, data.NumberMagnification);
        _partDate.text = data.IncreaserType.ToString();
    }

}
