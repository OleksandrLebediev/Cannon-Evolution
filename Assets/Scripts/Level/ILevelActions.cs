using UnityEngine.Events;

public interface ILevelActions
{
    public event UnityAction<int> LevelChanges;
    public event UnityAction<int> LevelRestarted;
    public event UnityAction<int> LevelStart;
}

