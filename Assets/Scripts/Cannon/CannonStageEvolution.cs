using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStageEvolution : MonoBehaviour
{
    [SerializeField] private CannonParameters _parameters;
    [SerializeField] private ParticleSystem _particleSystem;

    public CannonParameters Parameters => _parameters;
    public ParticleSystem ParticleSystem => _particleSystem;

    public void Show()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(1, 1).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
