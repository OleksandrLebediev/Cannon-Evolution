using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        _animator.enabled = false;
    }

    public void Explode(float force, Vector3 position, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cannon cannon))
        {
            cannon.TakeDamage();
        }
    }
}
