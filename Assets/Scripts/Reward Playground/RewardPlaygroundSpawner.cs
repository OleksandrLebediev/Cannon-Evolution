using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RewardPlaygroundSpawner : MonoBehaviour
{
    [SerializeField] private CannonYearDisplay _cannonYearDisplay;
    [SerializeField] private RewardPlayground _rewardPlaygroundTemplate;
    public RewardPlayground RewardPlayground { get; private set; }

    private ICannonMoverControl _cannonMoverControl;
    private ICannonShootingControl _cannonShootingControl;
    private readonly int _ofssetZ = 10;

    public event UnityAction RewardPlaygroundPassed;

    public void Initialize(ICannonMoverControl cannonMoverControl,
        ICannonShootingControl cannonShootingControl)
    {
        _cannonMoverControl = cannonMoverControl;
        _cannonShootingControl = cannonShootingControl;
    }

    public void Spawn(Vector3 position)
    {
        if (RewardPlayground != null)
            Remove(RewardPlayground);

        position.z += _ofssetZ; 
        RewardPlayground = Instantiate(_rewardPlaygroundTemplate, position, Quaternion.identity);
        RewardPlayground.Initialize(_cannonMoverControl, _cannonShootingControl, _cannonYearDisplay);
        RewardPlayground.Passed += OnPassed;
    }

    private void OnPassed()
    {
        RewardPlaygroundPassed.Invoke();
    }

    private void Remove(RewardPlayground rewardPlayground)
    {
        Destroy(rewardPlayground.gameObject);
    }
}
