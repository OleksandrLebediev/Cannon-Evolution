using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cannon : MonoBehaviour, IIncreases
{
    [SerializeField] private List<CannonStageEvolution> _stagesEvolution;

    private CannonStageEvolution _currentStageEvolution;
    private AudioSource _audioSource;
    private int _indexEvolution = 0;

    private CannonParameters Parameters => _currentStageEvolution.Parameters;
    private ParticleSystem ParticleSystem => _currentStageEvolution.ParticleSystem;
    public float RechargeTime => Parameters.RechargeTime;

    public event UnityAction<IncreaserData> PassedIncreaserZone;
    public event UnityAction Destroyed;

    public void Initialize(AudioSource audioSource)
    {
        _currentStageEvolution = _stagesEvolution[_indexEvolution];
        _audioSource = audioSource; 
    }

    public void Show()
    {
        _stagesEvolution[_indexEvolution].Hide();
        gameObject.SetActive(true);
        _stagesEvolution[_indexEvolution].Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Shoot()
    {
        Ball ball = Instantiate(Parameters.BallTemplate, Parameters.ShotPoint.position, Quaternion.identity);
        ball.Initialize(Parameters.SpeedBall, Parameters.Radius, Parameters.Force);
        _audioSource.pitch = Random.Range(0.8f, 1f);
        _audioSource.Play();
        ParticleSystem.Play();
        Destroy(ball.gameObject, Parameters.Range);
    }

    public void ApplyHit()
    {
        Destroyed?.Invoke();
    }

    public void ChangeLevelEvolution(int stageEvolution)
    {
        _stagesEvolution[_indexEvolution].Hide();
        _stagesEvolution[stageEvolution].Show();
        _indexEvolution = stageEvolution;
        _currentStageEvolution = _stagesEvolution[stageEvolution];
    }

    public void OnPassedIncreaserZone(IncreaserData increaserData)
    {
        PassedIncreaserZone?.Invoke(increaserData);
        Vibration.VibratePeek();
    }

}