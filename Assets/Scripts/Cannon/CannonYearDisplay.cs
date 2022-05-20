using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CannonYearDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _year;

    public void UpdateYearUI(int currentYear, int targetYear)
    {
        _year.text = targetYear.ToString();
    }
}
