using UnityEngine;
using UnityEngine.Events;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private LevelsSwitcher _levelsSwitcher;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private Player _player;
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private TouchHandler _joystick;
    [SerializeField] private CannonHandler _cannonHandler;
    
    private BinarySaveSystem _saveSystem;

    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private RewardPlaygroundSpawner _rewardPlaygroundSpawner;
    [SerializeField] private MoneyHendler _moneyHendler;

    private void Start()
    {
        Subscribe();
        Vibration.Init();

        _saveSystem = new BinarySaveSystem();
        SaveData data = _saveSystem.Load();

        UIInputData uIInputData = new UIInputData(_levelsSwitcher, _player.Wallet, _joystick);
        _uIManager.Initialize(_player, uIInputData, _player.Wallet);

        _joystick.Initialize(_uIManager);
        _player.Initialize(_joystick, _cannonHandler);
        _player.Wallet.Initialize(data.Money);
        _cannonHandler.Initialize(_joystick);
        _cameraFollower.Initialize(_player);
        _moneyHendler.Initialize(_player.Wallet);
        _levelSpawner.Initialize(_moneyHendler);
        _rewardPlaygroundSpawner.Initialize(_player.Movement, _cannonHandler);

        _levelsSwitcher.Initialize(_uIManager, data.Level);   
    }

    private void Subscribe()
    {
        _levelsSwitcher.LevelChanges += OnLevelChanges;
        _levelsSwitcher.LevelRestarted += OnLevelRestarted;
        _levelsSwitcher.LevelStart += OnLevelStart;
    }

    private void OnLevelStart(int level)
    {
        _levelSpawner.Spawn(level);
        _rewardPlaygroundSpawner.Spawn(_levelSpawner.CurrentLevel.LastPlatformPosition);
        _player.ResetPlayer();
        _cannonHandler.ResetCannons();

        TinySauce.OnGameStarted("level_" + level);
        SaveData data = new SaveData(level, _player.Wallet.AmountMoney);
        _saveSystem.Save(data);
    }

    private void OnLevelRestarted(int level)
    {
        TinySauce.OnGameFinished(false, _player.Wallet.AmountMoneyPerLevel, "level_" + level);
    }

    private void OnLevelChanges(int level)
    {
        TinySauce.OnGameFinished(true, _player.Wallet.AmountMoneyPerLevel, "level_" + level);
    }

    private void OnDestroy()
    {
        _levelsSwitcher.LevelChanges -= OnLevelChanges;
        _levelsSwitcher.LevelRestarted -= OnLevelRestarted;
        _levelsSwitcher.LevelStart -= OnLevelStart;
    }

    private void OnApplicationQuit()
    {
        SaveData data = new SaveData(_levelsSwitcher.CurrentLevelID, _player.Wallet.AmountMoney);
        _saveSystem.Save(data);
    }
}

