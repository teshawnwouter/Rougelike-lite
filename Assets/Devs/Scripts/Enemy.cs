using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour,IDamageable
{
    private int health = 1;

    public void TakeDamage(int Amount)
    {
        health -= Amount;
    }
}
