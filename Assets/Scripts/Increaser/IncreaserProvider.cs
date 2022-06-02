using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaserProvider : MonoBehaviour
{
    private Increaser[] _increasers;

    private void Awake()
    {
        _increasers = GetComponentsInChildren<Increaser>();
    }

    public void Initialize()
    {
        foreach (Increaser increaser in _increasers)
        {
            increaser.Initialize();
        }
    }
}
