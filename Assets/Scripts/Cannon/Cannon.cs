using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Ball _ballTemplate;
    [SerializeField] private float _speed;
    [SerializeField] private float _rechargeTime;

    public float RechargeTime => _rechargeTime;
    public event UnityAction<IncreaserData> PassedIncreaserYearsZone;
    public event UnityAction<IncreaserData> PassedIncreaserNumberZone;
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPassedIncreaserYearsZone(IncreaserData increaserData)
    {
        PassedIncreaserYearsZone?.Invoke(increaserData);
    }
    
    public void OnPassedIncreaserNumberZone(IncreaserData increaserData)
    {
        PassedIncreaserNumberZone?.Invoke(increaserData);
    }

    public void TakeDamage()
    {
        Hide();
    }

    public void Shoot()
    {
        Ball ball = Instantiate(_ballTemplate, _shotPoint.position, Quaternion.identity);
        ball.Initialize(_shotPoint, _speed);
        Destroy(ball.gameObject, _rechargeTime);
    }
}
