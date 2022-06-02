using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(PlayerWallet))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IPlayerEvents
{
    [SerializeField] private List<Cannon> _cannons;

    public PlayerWallet Wallet { get; private set; }
    public PlayerMovement Movement { get; private set; }

    private CannonHandler _cannonHandler;
    private Vector3 _startPosition = new Vector3(0, 0, 0);

    public event UnityAction AllCannonsLost;

    private void Awake()
    {
        Wallet = GetComponent<PlayerWallet>();
        Movement = GetComponent<PlayerMovement>();    }

    public void Initialize(TouchHandler joystickControl,
        CannonHandler cannonHandler)
    {
        Movement.Initialize(joystickControl, cannonHandler);
        _cannonHandler = cannonHandler;

        cannonHandler.AllCannonsDestroy += OnAllCannonsDestroy;

    }

    public void ResetPlayer()
    {
        transform.position = _startPosition;
    }

    private void OnAllCannonsDestroy()
    {
        Movement.StopMoving();
        AllCannonsLost?.Invoke();
    }


    private void OnDestroy()
    {
        _cannonHandler.AllCannonsDestroy -= OnAllCannonsDestroy;
    }
}
