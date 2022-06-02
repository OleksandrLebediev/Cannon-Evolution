using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICannonMoverControl
{
    public IEnumerator MoveToTargetCoroutine(Vector3 target);
    public void StopMoving();
}

