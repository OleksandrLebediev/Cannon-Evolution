using System;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Ball : MonoBehaviour
{
    private float _speed;
    private Rigidbody _rigidbody;
    private bool _isMoving;
    private float _radius;
    private float _force;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(float speedBall, float radius, float force)
    {
        _speed = speedBall;
        _radius = radius;
        _force = force;
        _isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void FixedUpdate()
    {
        if (_isMoving == false) return;
        _rigidbody.velocity = transform.forward * _speed;
    }

    private void Explode()
    {
        Collider[] overlapCollider = Physics.OverlapSphere(transform.position, _radius);
        for (int i = 0; i < overlapCollider.Length; i++)
        {
            if (overlapCollider[i].TryGetComponent<IPhysicalTarget>(out IPhysicalTarget target))
            {
                target.OnCollision(_force, transform.position, _radius);
                _isMoving = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
