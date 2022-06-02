using UnityEngine;
using UnityEngine.Events;


public class StagingZone : MonoBehaviour
{
    public event UnityAction CannonInZone;
    private bool _isImside;

    private void OnTriggerEnter(Collider other)
    {
        if (_isImside == true) return;

        if (other.TryGetComponent<Cannon>(out Cannon finishes))
        {
            CannonInZone?.Invoke();
            _isImside = true;
        }
    }
}

