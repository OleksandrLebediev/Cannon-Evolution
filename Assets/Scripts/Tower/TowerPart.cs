using UnityEngine;

public class TowerPart : MonoBehaviour, IPhysicalTarget
{
    private Rigidbody _rigidbody;
    private ITowerHitHandler _hitHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(ITowerHitHandler hitHandler)
    {
        _hitHandler = hitHandler;
        _rigidbody.mass = 1f;
    }

    public void EnablePhysical()
    {
        if (_rigidbody != null)
            _rigidbody.isKinematic = false;
    }

    public void DisablePhysical()
    {
        _rigidbody.isKinematic = true;
    }

    public void OnCollision(float force, Vector3 position, float radius)
    {
        EnablePhysical();
        _rigidbody.AddExplosionForce(force, position, radius);
        _hitHandler.OnHit();
    }

    public void TryDestroy(int time)
    {
        Destroy(gameObject, time);
    }

}
