using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private List<Level> _levels = new List<Level>();
    [SerializeField] Level _level;
    public Level CurrentLevel => _level;
    public int LevelsCount => _levels.Count;

    private MoneyHendler _moneyHendler;

    public void Initialize(MoneyHendler moneyHendler)
    {
        _moneyHendler = moneyHendler;
    }

    public void Spawn(int levelID)
    {
        //_level = GameObject.FindObjectOfType<Level>();
        if (_level != null)
            RemoveLevel(_level);

        _level = Instantiate(_levels[levelID]);
        _level.Initialize(_moneyHendler);
    }


    private void RemoveLevel(Level level)
    {
        Destroy(level.gameObject);
    }
}
