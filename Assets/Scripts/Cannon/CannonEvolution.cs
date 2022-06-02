using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonEvolution
{
    private int _yearСoefficient = 500;
    private int _countEvolutions = 3;

    public int GetStageEvolution(int year)
    {
        if(year < 0) return 0;

        for (int i = 0; i < _countEvolutions - 1; i++)
        {
            bool inRange = year >= _yearСoefficient * i &&
                year < _yearСoefficient * (i + 1);
            if (inRange == true)
                return i;
            else
                continue;
        }
        return _countEvolutions - 1;
    }
}
