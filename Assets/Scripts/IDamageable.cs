using UnityEngine;

public interface IDamageable
{
    public void OnHit(int damage, Vector2 knockback);
    public void OnHit(int damage);
    public void OnObjectDestroyed();
}
