using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class CannonHandler : MonoBehaviour, ICannonShootingControl
{
    [SerializeField] private CannonYearDisplay _cannonYearDisplay;
    [SerializeField] private List<Cannon> _cannons;
    [SerializeField] private AudioSource _audioSource;
    
    private List<Cannon> _cannonsSpawned = new List<Cannon>();
    private ArithmeticOperatorsHandler _arithmetic = new ArithmeticOperatorsHandler();
    private CannonEvolution _cannonEvolution = new CannonEvolution();
    private Coroutine _shootingCoroutine;
    private ITouchHandler _tapHandler;
    private int _startCountCannons = 1;
    private int _startLevelEvolution = 0;
    private int _currentYear;
    private int _currentCountCannon;
    private int _currentLevelEvolution;
    private float _platformLimit = 4;

    public float MaxHorizontalPoint { get; private set; }
    public float MinHorizontalPoint { get; private set; }

    public event UnityAction AllCannonsDestroy;

    public void Initialize(ITouchHandler firstTapHandler)
    {
        foreach (var cannon in _cannons)
        {
            cannon.PassedIncreaserZone += OnCannonPassedIncreaserZone;
            cannon.Destroyed += OnCannonDestroyed;
            cannon.Initialize(_audioSource);
        }

        _tapHandler = firstTapHandler;
        _tapHandler.FirstTouch += OnFirstTap;
    }

    private void OnFirstTap()
    {
        if (_shootingCoroutine != null) 
            StopCoroutine(_shootingCoroutine); 
        StartShooting();
    }

    private void OnDestroy()
    {
        foreach (var cannon in _cannons)
        {
            cannon.PassedIncreaserZone -= OnCannonPassedIncreaserZone;
            cannon.Destroyed -= OnCannonDestroyed;
        }

        _tapHandler.FirstTouch -= OnFirstTap;
    }

    public void ResetCannons()
    {
        ChangeCountCannon(_startCountCannons);
        ChangeLevelEvolutionCannon(_startLevelEvolution);
        _currentCountCannon = _startCountCannons;
        _currentLevelEvolution = _startLevelEvolution;
        _currentYear = 0;

        _cannonYearDisplay.ResetYear();
        _cannonYearDisplay.Show();
    }

    public void StartShooting(bool multiple = true, int numberShots = 0)
    {
        _shootingCoroutine =  StartCoroutine(ShootingCannonsCoroutine(multiple, numberShots));
    }
    
    public void StopShooting()
    {
       if(_shootingCoroutine != null)
           StopCoroutine(_shootingCoroutine);
    }
    
    private void OnCannonDestroyed()
    {
        _currentCountCannon--;
        ChangeCountCannon(_currentCountCannon);
        if (_currentCountCannon <= 0)
        {
            AllCannonsDestroy?.Invoke();
            StopAllCoroutines();;
        }
    }
    
    private void OnCannonPassedIncreaserZone(IncreaserData increaserData)
    {
        switch (increaserData.IncreaserType)
        {
            case IncreaserType.Year:
                IncreaseYearsCannon(increaserData);
                break;
            case IncreaserType.Cannon:
                IncreaseCountCannon(increaserData);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void IncreaseYearsCannon(IncreaserData increaserData)
    {
        int nextYear = _arithmetic.Calculation(increaserData.ArithmeticOperator,
            _currentYear, increaserData.NumberMagnification);
        _cannonYearDisplay.UpdateYear(nextYear);
        int levelEvolution = _cannonEvolution.GetStageEvolution(nextYear);
        _currentYear = nextYear;

        if (levelEvolution != _currentLevelEvolution)
        {
            ChangeLevelEvolutionCannon(levelEvolution);
            _currentLevelEvolution = levelEvolution;
        }
       
    }

    private void IncreaseCountCannon(IncreaserData increaserData)
    {
        int nextNumberCannon = _arithmetic.Calculation(increaserData.ArithmeticOperator,
            _currentCountCannon, increaserData.NumberMagnification);
        ChangeCountCannon(nextNumberCannon);
        _currentCountCannon = nextNumberCannon;
    }
    
    private void ChangeLevelEvolutionCannon(int stageEvolution)
    {
        foreach (var cannon in _cannonsSpawned)
        {
            cannon.ChangeLevelEvolution(stageEvolution);
        }
    }

    private void ChangeCountCannon(int countCannon)
    {
        _cannonsSpawned.Clear();
        int _index = 0;

        foreach (var cannon in _cannons)
        {
            if(_index < countCannon)
            {
                cannon.ChangeLevelEvolution(_currentLevelEvolution);
                cannon.Show();
                _cannonsSpawned.Add(cannon);
            }
            else
            {
                cannon.Hide();
            }
            _index++;
        }

        UpdateHorizontalLimits();
    }

    private void UpdateHorizontalLimits()
    {
        float maxPoint = 0;
        float minPoint = 0;
        float cuurrenPoint = 0; 

        foreach (var cannon in _cannonsSpawned)
        {
            cuurrenPoint = cannon.transform.localPosition.x;

            if (maxPoint < cuurrenPoint)
            {
                maxPoint = cuurrenPoint;
            }

            if (minPoint > cuurrenPoint)
            {
                minPoint = cuurrenPoint;
            }
        }

        MaxHorizontalPoint = _platformLimit - maxPoint;
        MinHorizontalPoint = -1 * minPoint - _platformLimit;
    }

    private IEnumerator ShootingCannonsCoroutine(bool multiple, int numberShots)
    {
        int currentShot = 0;

        while (multiple == true || currentShot < numberShots)
        {
            foreach (var cannon in _cannonsSpawned)
            {
                cannon.Shoot();
            }
            currentShot++;
            yield return new WaitForSeconds(_cannonsSpawned[0].RechargeTime);
            yield return null;
        }
    }
}
