using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileSpells : MonoBehaviour
{
    [Header("spell properties")]
    private float spellSpeed = 6f;
    private int spellDamage;
    [Header("components")]
    private Rigidbody2D rb;
    public Spells spells;
    private Player player;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        spellDamage = spells.Value;

        if (player.isFacingRight)
        {
            rb.velocity = new Vector2(spellSpeed, 0);
            transform.localScale = new Vector2(gameObject.transform.localScale.x * 1, transform.localScale.y);
        }
        else
        {
            rb.velocity = new Vector2(-spellSpeed, 0);
            transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damagable = collision.GetComponent<IDamageable>();
        if (damagable != null) 
        {
            damagable.TakeDamage(spellDamage);
            Destroy(gameObject);
        }
    }
}
