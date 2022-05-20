using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class CannonHandler : MonoBehaviour
{
    [SerializeField] private Cannon[] _cannons;
    private Cannon _currentCannon;
    private int _currentLevelEvolution;
    private float StartPositionX;
    
    public event UnityAction<IncreaserData> CannonPassedIncreaserYears;
    public event UnityAction<IncreaserData> CannonPassedIncreaserNumber;
    
    public void Initialize()
    {
        if(gameObject.activeSelf == false) return;
        
        _cannons[_currentLevelEvolution].Show();
        _currentCannon = _cannons[_currentLevelEvolution];
        StartPositionX = transform.position.x;
        StartCoroutine(Shoot());
        foreach (var cannon in _cannons)
        {
            cannon.PassedIncreaserNumberZone += OnPassedIncreaserNumber;
            cannon.PassedIncreaserYearsZone += OnPassedIncreaserYears;
        }
    }

    private void OnDestroy()
    {
        foreach (var cannon in _cannons)
        {
            cannon.PassedIncreaserNumberZone -= OnPassedIncreaserNumber;
            cannon.PassedIncreaserYearsZone -= OnPassedIncreaserYears;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void ChangeLevel(int nextlevel)
    {
        _cannons[_currentLevelEvolution].Hide();
        _currentLevelEvolution++;
        _cannons[nextlevel].Show();
        _currentCannon = _cannons[nextlevel];
        _currentLevelEvolution = nextlevel;
    }

    public float GetPositionX()
    {
        return transform.position.x + StartPositionX;
    }
    
    public void OnPassedIncreaserYears(IncreaserData increaserData)
    {
        CannonPassedIncreaserYears?.Invoke(increaserData);       
    }
    
    public void OnPassedIncreaserNumber(IncreaserData increaserData)
    {
        CannonPassedIncreaserNumber?.Invoke(increaserData);       
    }
    
    private IEnumerator Shoot()
    {
        WaitForSeconds recharge = new WaitForSeconds(_currentCannon.RechargeTime);
        
        while (true)
        {
            _currentCannon.Shoot();
            yield return recharge;
        }
    }
}
