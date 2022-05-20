using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleIncreaser : MonoBehaviour
{
    private Increaser[] _increasers;
    
    private void Awake()
    {
        _increasers = GetComponentsInChildren<Increaser>();
    }
    
    
}
