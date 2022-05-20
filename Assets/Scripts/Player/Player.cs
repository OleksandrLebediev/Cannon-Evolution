using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CannonProvider _cannonProvider;
    private EvolutionHandler _evolutionHandler;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedHorizontal;

    private void Awake()
    {
        _evolutionHandler = new EvolutionHandler();
    }

    private void Start()
    {
        _cannonProvider.Initialize();
        _cannonProvider.YearChanged += OnTryEvolution;
    }

    private void OnTryEvolution(int year)
    {
        int levelEvolution = _evolutionHandler.TryEvolution(year);
        _cannonProvider.CannonsEvolution(levelEvolution);
    }

    private void Update()
    {
        float point = _cannonProvider.CheckCannonInside();
        float input = Input.GetAxis("Horizontal") * _speedHorizontal * Time.deltaTime;

        if (input > 0 && point > 0 || input < 0 && point < 0) return;
        transform.Translate(input, 0, _speed * Time.deltaTime);
    }
} 
