using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, ICannonMoverControl
{
    [SerializeField] private float _speedHorizontal;
    [SerializeField] private float _speedForward;
    
    private ITouchHandler _touchHandler;
    private CannonHandler _cannonHandler;
    private bool _isMoving = false;
    private float _horizontalPosition; 
    private Vector3 _movePosition = Vector3.zero;
    
    public void Initialize(ITouchHandler touchHandler, CannonHandler cannonHandler)
    {
        _touchHandler = touchHandler;
        _cannonHandler = cannonHandler;
        _touchHandler.MovingTouch += Move;
        _touchHandler.FirstTouch += OnFirstTap;
    }
   
    private void OnDisable()
    {
        _touchHandler.MovingTouch -= Move;
    }

    private void Update()
    {
        if (_isMoving == false) return;
        transform.Translate(0, 0, _speedForward * Time.deltaTime);
    }

    private void OnFirstTap()
    {
        StartMoving();
    }

    private void Move(float direction)
    {
        if (_isMoving == false) return;

        _horizontalPosition = direction * _speedHorizontal * Time.deltaTime;
        _movePosition = transform.position;
        _movePosition.x += _horizontalPosition;
        _movePosition.x = Mathf.Clamp(_movePosition.x, _cannonHandler.MinHorizontalPoint, _cannonHandler.MaxHorizontalPoint);
        transform.position = _movePosition;
    }

    public void StartMoving()
    {
        _isMoving = true;
    }
    
    public void StopMoving()
    {
        _isMoving = false;
    }
    
    public IEnumerator MoveToTargetCoroutine(Vector3 target)
    {
        target.y = 0;

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speedForward * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) <= 0.01f) yield break;
            yield return null;
        }
    }
}
