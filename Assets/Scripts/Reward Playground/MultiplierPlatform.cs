using UnityEngine;

public class MultiplierPlatform : MonoBehaviour
{
    [SerializeField] private ConfettiEffect[] _confettiEffect;

    private ITargetOfReward[] _targetOfReward;
    private StopLine _stopLine;
    public Vector3 StopPositon => _stopLine.Position;

    private void Awake()
    {
        _targetOfReward = GetComponentsInChildren<ITargetOfReward>();
        _stopLine = GetComponentInChildren<StopLine>();
    }

    public void InitializeTargetOfReward()
    {
        foreach (var target in _targetOfReward)
        {
            target.Hide();
            target.Initialize();
        }
    }

    public void ShowTargetOfReward()
    {
        foreach (var target in _targetOfReward)
        {
            target.Show();
        }
    }

    public void PlayConfettiEffect()
    {
        foreach (var confetti in _confettiEffect)
        {
            confetti.Play();
        }
    }

    public int CheckDefect()
    {
        int defects = 0;

        foreach (var target in _targetOfReward)
        {
            defects += target.GetDefect();
        }
        if (defects == 0) return 0;
        return defects /= _targetOfReward.Length;
    }
}
