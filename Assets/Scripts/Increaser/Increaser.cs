using System;
using UnityEngine;

public class Increaser : MonoBehaviour
{
    [SerializeField] private IncreaserType _increaserType; 
    [SerializeField] private IncreaserData _increaserData;
    private IncreaserZone _increaserZone;
    private IncreaserDisplay _display;
    private bool _isCrossed;
    private void Awake()
    {
        _increaserZone = GetComponentInChildren<IncreaserZone>();
        _display = GetComponentInChildren<IncreaserDisplay>();
    }

    private void Start()
    {
        _increaserZone.CannonPassedIncreaserZone += OnCannonPassedIncreaserZone;
        _display.Initialize(_increaserData);
    }

    private void OnDisable()
    {
        _increaserZone.CannonPassedIncreaserZone -= OnCannonPassedIncreaserZone;
    }

    private void OnCannonPassedIncreaserZone(Cannon cannon)
    {
        if(_isCrossed == true) return;
        
        switch (_increaserType)
        {
            case IncreaserType.Year:
                cannon.OnPassedIncreaserYearsZone(_increaserData);
                break;
            case IncreaserType.Number:
                cannon.OnPassedIncreaserNumberZone(_increaserData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _isCrossed = true;
    }
}

public enum IncreaserType
{
    Year,
    Number
}