using UnityEngine;
using UnityEngine.Events;

public class IncreaserZone : MonoBehaviour
{
    public event UnityAction<IIncreases> CannonPassedIncreaserZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IIncreases>(out IIncreases increases))
        {
            CannonPassedIncreaserZone?.Invoke(increases);
        }
    }
}
