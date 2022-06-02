using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _lastPlatformPosition;

    private EnemiesProvider _enemiesProvider;
    private IncreaserProvider _icreaserProvider;
    private AudioSource _audioSource;

    public Vector3 LastPlatformPosition => _lastPlatformPosition.position;

    private void Awake()
    {
        _enemiesProvider = GetComponentInChildren<EnemiesProvider>();
        _icreaserProvider = GetComponentInChildren<IncreaserProvider>();
        _audioSource = GetComponent<AudioSource>();   
    }

    public void Initialize(MoneyHendler moneyHendler)
    {
        _enemiesProvider.Initialize(moneyHendler, _audioSource);
        _icreaserProvider.Initialize();
    }

}
