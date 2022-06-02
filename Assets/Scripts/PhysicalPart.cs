using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicalPart : MonoBehaviour, IPhysicalTarget
{
    private ICharacterHitHandler _hitHandler;
    private Rigidbody _rigidbody;
    private CharacterJoint _characterJoint;

    private Vector3 _connectedAnchor;
    private Vector3 _anchor;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _characterJoint = GetComponent<CharacterJoint>();    
    }
    
    public void Initialize(ICharacterHitHandler hitHandler)
    {
        _hitHandler = hitHandler;

        if (_characterJoint == null) return;
        _connectedAnchor = _characterJoint.connectedAnchor;
        _anchor = _characterJoint.anchor;
    }
    
    public void OnCollision(float force, Vector3 position, float radius)
    {
       _hitHandler.OnHit(force, position,radius, _rigidbody);
    }

    public void EnablePhysical()
    {
        _rigidbody.isKinematic = false;
    }
    
    public void DisablePhysical()
    {
        _rigidbody.isKinematic = true;
    }

    public void CorectCharacterJoint()
    {
        if (_characterJoint == null) return;
        _characterJoint.connectedAnchor = _connectedAnchor;
        _characterJoint.anchor = _anchor;
    }
}
