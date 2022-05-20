using UnityEngine;
using UnityEngine.Events;

public class IncreaserZone : MonoBehaviour
{
    public event UnityAction<Cannon> CannonPassedIncreaserZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Cannon>(out Cannon cannon))
        {
            CannonPassedIncreaserZone?.Invoke(cannon);
        }
    }
}
