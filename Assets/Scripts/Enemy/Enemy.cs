using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ICharacterHitHandler, ITargetOfReward
{
    [SerializeField] private PhysicalPart[] _physicalParts;
    private Animator _animator;
   
    private bool _isDead;
    private bool _isHit;
    private Vector3 _startPosition;

    public event UnityAction<Vector3> Dead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDead == true) return;
        if (other.TryGetComponent(out Cannon cannon))
        {
            cannon.ApplyHit();
        }
    }

    public void Initialize()
    {
        foreach (var part in _physicalParts)
        {
            part.Initialize(this);
            part.DisablePhysical();
        }

        _startPosition = transform.position;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(1, 1).OnComplete(CorectCharacterJoint);
    }

    public void Hide()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void OnHit(float force, Vector3 position, float radius, Rigidbody rigidbody)
    {
        _animator.enabled = false;
        _isDead = true;
        MakePhysical();
        rigidbody.AddExplosionForce(force, position, radius);

        Destroy(gameObject, 3);

        if (_isHit == false)
        {
            Vibration.VibratePeek();
            _isHit = true;
            Dead?.Invoke(_startPosition);
        }
    }


    private void MakePhysical()
    {
        foreach (var part in _physicalParts)
        {
            part.EnablePhysical();
        }
    }

    private void CorectCharacterJoint()
    {
        foreach (var part in _physicalParts)
        {
            part.CorectCharacterJoint();
        }
    }

    public int GetDefect()
    {
        if (_isDead) return 100;
        return 0;
    }
}
