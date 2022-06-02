using DG.Tweening;
using TMPro;
using UnityEngine;

public class CannonYearDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _year;

    private int _currentYear;
    private float _durationIncrementAnimation = 1;

    public void UpdateYear(int year)
    {
        ProfitIncrementAnimation(year);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ResetYear()
    {
        _currentYear = 0;
        _year.text = _currentYear.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);       
    }


    private void ProfitIncrementAnimation(int currentYear)
    {
        DOTween.To(() => _currentYear, x => _currentYear = x,
            currentYear, _durationIncrementAnimation).OnUpdate(YearInput);
    }

    private void YearInput()
    {
        _year.text = _currentYear.ToString();
    }

}
