using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    private Player _target;

    public void Initialize(Player target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target == null) return;
        
        Vector3 offsetTarget = _target.transform.position + _offset;
        offsetTarget.x = 0;
        transform.position = offsetTarget;
    }
}
