using UnityEngine;

public class Attacks : MonoBehaviour
{
    [Header("damage")]
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
            damageable.TakeDamage(damage);
    }
}
