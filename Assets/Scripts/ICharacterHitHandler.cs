using UnityEngine;
public interface ICharacterHitHandler 
{
    public void OnHit(float force, Vector3 position, float radius, Rigidbody rigidbody);
}
