using System;
using UnityEngine;
public class RuiGolem : Enemy
{
    public void Awake()
    {
        detection = GetComponent<Detection>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        //variables from main script
        health = 9;
        walkSpeed = 5f;
        abletoMoveVector = new Vector2(-1, 0);
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
