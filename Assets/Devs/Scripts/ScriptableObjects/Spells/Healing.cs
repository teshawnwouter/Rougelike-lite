using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    private int healingAmont;

    public Spells spell;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        healingAmont = spell.Value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.health <= player.maxHealth) 
        { 
        player.health += healingAmont;
        }
       Destroy(gameObject);

    }
}
