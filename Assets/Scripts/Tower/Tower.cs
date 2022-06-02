using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITowerHitHandler
{
    private TowerPart[] _parts;
    private bool _isDestroyed;
    private int _numberDmagedParts;
    private int _numberParts;

    private void Awake()
    {
        _parts = GetComponentsInChildren<TowerPart>();
        
    }

    private void Start()
    {
        Initialize();
        _numberParts = _parts.Length;
    }

    public void Initialize()
    {
        foreach (var part in _parts)
        {
            part.Initialize(this);
            part.DisablePhysical();
        }
    }

    public void OnHit()
    {
        _numberDmagedParts++;
        if (CheckDestroyed() == false) return;

        _isDestroyed = true;
        foreach (var part in _parts)
        {
            part.EnablePhysical();
            part.TryDestroy(5);
        }

    }

    private bool CheckDestroyed()
    {
        return _numberDmagedParts >= _numberParts / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDestroyed == true) return;
        if (other.TryGetComponent(out Cannon cannon))
        {
            cannon.ApplyHit();
        }
    }
}
