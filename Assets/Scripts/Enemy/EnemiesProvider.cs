using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesProvider : MonoBehaviour
{
    private Enemy[] _enemies;
    private MoneyHendler _moneyHendler;
    private AudioSource _audioSource;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>();
    }

    public void Initialize(MoneyHendler moneyHendler, AudioSource audioSource)
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Initialize();
            enemy.Dead += OnEnemyDead;
        }
        _moneyHendler = moneyHendler;
        _audioSource = audioSource;
    }

    private void OnEnemyDead(Vector3 position)
    {
        _moneyHendler.GetMoney(10, position);
        _audioSource.pitch = Random.Range(0.8f, 1.0f);
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.Dead -= OnEnemyDead;
        }
    }
}
