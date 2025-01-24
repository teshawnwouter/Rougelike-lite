using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Enemy[] enemy;
    private Bat[] bat;

    public Spells spells;
    private int damage;

    public LayerMask layerMask;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        enemy = FindObjectsOfType<Enemy>();
        bat = FindObjectsOfType<Bat>();
        damage = spells.Value;
    }

    private void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BOOM"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] explosionZone = Physics2D.OverlapCircleAll(transform.position, 8,layerMask);
        foreach (Collider2D enemy in explosionZone)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
                damageable.TakeDamage(damage);
        }
    }
}
