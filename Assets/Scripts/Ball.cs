using System;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Ball : MonoBehaviour
{
    private float _speed;
    private Rigidbody _rigidbody;
    private bool _isMoving;
    private float _radius = 2;
    private float _force = 1000;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(Transform shotPoint, float speed)
    {
        _speed = speed;
        _isMoving = true;
    }

    private void FixedUpdate()
    {
        if (_isMoving == false) return;
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isMoving = false;
        _rigidbody.useGravity = true;
        Explode();
    }

    private void Explode()
    {
        Collider[] overlapCollider = Physics.OverlapSphere(transform.position, _radius);
        for (int i = 0; i < overlapCollider.Length; i++)
        {
            if(overlapCollider[i].TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakeDamage();
                enemy.Explode(_force, transform.position, _radius);
            }
           // Rigidbody rigidbody = overlapCollider[i].attachedRigidbody;
           // if (rigidbody)
           // {
           //     rigidbody.AddExplosionForce(_force, transform.position, _radius);
           // }
        }
    }
}
