using System;
using UnityEngine;
using static Enemy;

public class RuiGolem : Enemy
{
    GameObject ruinGolem;
    public void Awake()
    {
        detection = GetComponent<Detection>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        health = 9;
        walkSpeed = 2.5f;
    }


    private void FixedUpdate()
    {
        if (detection.isGrounded && detection.isOnwall) 
        {
            FlipDirection();
        }
        rigidbody2D.velocity = new Vector2(walkSpeed * abletoMoveVector.x, rigidbody2D.velocity.y);
    }
}
