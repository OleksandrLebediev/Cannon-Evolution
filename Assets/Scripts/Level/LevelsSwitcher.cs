using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LevelSpawner))]
public class LevelsSwitcher : MonoBehaviour, ILevelsInformant, ILevelActions
{
    private IUIAnswer _uIAnswer;
    private int _currentLevel;
    private LevelSpawner _spawner;
  
    public int CurrentLevelID => _currentLevel;

    public event UnityAction<int> LevelChanges;
    public event UnityAction<int> LevelRestarted;
    public event UnityAction<int> LevelStart;

    private void Awake()
    {
        _spawner = GetComponent<LevelSpawner>();
    }

    private void OnDestroy()
    {
        _uIAnswer.PressedRestartButton -= RestartLevel;
        _uIAnswer.PressedNextButton -= StartNextLevel;
    }

    public void Initialize(IUIAnswer uIAnswer, int currentLevel)
    {
        _currentLevel = currentLevel;
        _uIAnswer = uIAnswer;
        _uIAnswer.PressedRestartButton += RestartLevel;
        _uIAnswer.PressedNextButton += StartNextLevel;
        StartLevel(currentLevel);
    }

    private void StartLevel(int level)
    {
        LevelStart?.Invoke(level);
    }

    private void StartNextLevel()
    {
        LevelChanges?.Invoke(_currentLevel);
        _currentLevel++;
        if (_currentLevel >= _spawner.LevelsCount)
        {
            _currentLevel = Random.Range(0, _spawner.LevelsCount);
        }
        StartLevel(_currentLevel);
    }

    private void RestartLevel()
    {
        LevelRestarted?.Invoke(_currentLevel);
        StartLevel(_currentLevel);
    }
}
