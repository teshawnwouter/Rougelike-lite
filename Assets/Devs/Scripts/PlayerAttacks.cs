using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    [Header("Damage")]
    private int damage = 10;
    private float range = 1;

    [Header("Attack en Detections")]
    public Transform attackPoint;
    public LayerMask enemyMask;
    public bool isAttacking = false;

    public void InAttackMode(bool isInAttackingMode)
    {
        isAttacking = isInAttackingMode;
    }

    /// <summary>
    /// Get all enemies within circle to damage them
    /// </summary>
    public void Hit()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyMask);
        for (int i = 0; i < enemies.Length; i++)
        {
            IDamageable enemy = enemies[i].GetComponent<IDamageable>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void StopAttacking()
    {
        InAttackMode(false);
    }
}
