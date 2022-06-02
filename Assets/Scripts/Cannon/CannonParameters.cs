using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CannonParameters 
{
    [SerializeField] private Transform _shotPoint;
    [SerializeField] private Ball _ballTemplate;
    [SerializeField] private float _speedBall;
    [SerializeField] private float _radius;
    [SerializeField] private float _force;
    [SerializeField] private int _range;
    [SerializeField] private float _rechargeTime;

    public Transform ShotPoint => _shotPoint;
    public Ball BallTemplate => _ballTemplate;
    public float SpeedBall => _speedBall;
    public float Radius => _radius;
    public float Force => _force;
    public int Range => _range; 
    public float RechargeTime => _rechargeTime;
}
