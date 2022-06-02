using UnityEngine;
public interface IPhysicalTarget
{
    public void OnCollision(float force, Vector3 position, float radius);
    public void EnablePhysical();
    public void DisablePhysical();
}
