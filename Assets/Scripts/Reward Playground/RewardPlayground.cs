using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RewardPlayground : MonoBehaviour
{
    [SerializeField] private MultiplierPlatform[] _multiplierPlatform;
    [SerializeField] private StagingZone _stagingZone;
    
    private CannonYearDisplay _yearDisplay;
    private ICannonMoverControl _cannonMoverControl;
    private ICannonShootingControl _cannonShootingControl;
    private int _currentPlatformIndex;
    private AudioSource _audioSource;

    public event UnityAction Passed;

    private void Awake()
    {
        _multiplierPlatform = GetComponentsInChildren<MultiplierPlatform>();
        _audioSource = GetComponent<AudioSource>();     
    }

    public void Initialize(ICannonMoverControl cannonMoverControl, 
        ICannonShootingControl cannonShootingControl, CannonYearDisplay yearDisplay)
    {
        _stagingZone.CannonInZone += OnCannonInStagingZone;
        _cannonMoverControl = cannonMoverControl;
        _cannonShootingControl = cannonShootingControl;
        _yearDisplay = yearDisplay;

        foreach (var platform in _multiplierPlatform)
        {
            platform.InitializeTargetOfReward();
        }
    }

    private void OnCannonInStagingZone()
    {
        StartCoroutine(RewardPlaygroundScenario());
    }

    private IEnumerator RewardPlaygroundScenario()
    {
        while (true)
        {
            MultiplierPlatform multiplier = _multiplierPlatform[_currentPlatformIndex];

            _cannonMoverControl.StopMoving();
            _cannonShootingControl.StopShooting();

            yield return StartCoroutine(
                _cannonMoverControl.MoveToTargetCoroutine(multiplier.StopPositon));
            _yearDisplay.Hide();

            multiplier.ShowTargetOfReward();
            yield return new WaitForSeconds(1f);
            _cannonShootingControl.StartShooting(false, 1);
            yield return new WaitForSeconds(1f);
            int defect = multiplier.CheckDefect();
            _multiplierPlatform[_currentPlatformIndex].PlayConfettiEffect();
            _audioSource.Play();
            yield return new WaitForSeconds(1f);

            if (defect < 100 || _currentPlatformIndex == _multiplierPlatform.Length - 1)
            {
                Passed?.Invoke();
                yield break;
            }
            _currentPlatformIndex++;
            yield return null;
        }

    }

    private void OnDestroy()
    {
        _stagingZone.CannonInZone -= OnCannonInStagingZone;
    }

}
