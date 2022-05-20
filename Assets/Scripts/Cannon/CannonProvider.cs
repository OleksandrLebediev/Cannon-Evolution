using UnityEngine;
using UnityEngine.Events;

public class CannonProvider : MonoBehaviour
{
    [SerializeField] private CannonHandler[] _cannonsHandlers;
    [SerializeField] private CannonYearDisplay _cannonYearDisplay;
    private ArithmeticOperatorsHandler _arithmetic = new ArithmeticOperatorsHandler();
    private int _currentYear;
    private int _currentNumberCannon;
    
    public event UnityAction<int> YearChanged ;
    
    public void Initialize()
    {
        foreach (var cannon in _cannonsHandlers)
        {
            cannon.Initialize();
            cannon.CannonPassedIncreaserYears+= OnCannonPassedIncreaserYears;
            cannon.CannonPassedIncreaserNumber += OnCannonPassedIncreaserNumber;
        }
    }
    private void OnDestroy()
    {
        foreach (var cannon in _cannonsHandlers)
        {
            cannon.CannonPassedIncreaserYears -= OnCannonPassedIncreaserYears; 
            cannon.CannonPassedIncreaserNumber -= OnCannonPassedIncreaserNumber;
        }
    }

    public float CheckCannonInside()
    {
        float position = 0;

        foreach (var cannon in _cannonsHandlers)
        {
            float cannonXPosition = cannon.GetPositionX();
            if (cannonXPosition > 5.6 || cannonXPosition < -5.6)
            {
                position = cannonXPosition;
            }
        }
        return position;
    }
    
    public void CannonsEvolution(int level)
    {
        foreach (var cannon in _cannonsHandlers)
        {
            cannon.ChangeLevel(level);
        }
    }

    public void ChangeNumberCannon(int numberCannon)
    {
        for (int i = 0; i < _cannonsHandlers.Length; i++)
        {
            if (i < numberCannon)
            {
                _cannonsHandlers[i].Show();
                _cannonsHandlers[i].Initialize();
            }
            else
            {
                _cannonsHandlers[i].Hide();
            }
        }
    }
    
    private void OnCannonPassedIncreaserYears(IncreaserData increaserData)
    {
        int nextYear = _arithmetic.Calculation(increaserData.ArithmeticOperator,
            _currentYear, increaserData.NumberMagnification);
        _cannonYearDisplay.UpdateYearUI(_currentYear, nextYear);
        YearChanged?.Invoke(nextYear);
        _currentYear = nextYear;
    }

    private void OnCannonPassedIncreaserNumber(IncreaserData increaserData)
    {
        int nextNumberCannon = _arithmetic.Calculation(increaserData.ArithmeticOperator,
            _currentNumberCannon, increaserData.NumberMagnification);
        ChangeNumberCannon(nextNumberCannon);
    }
}
