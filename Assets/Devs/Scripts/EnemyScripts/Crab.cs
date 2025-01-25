using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crab : Enemy
{
    public void Awake()
    {
        //components
        attackZone = GetComponentInChildren<DetectionZone>();
        animator = GetComponent<Animator>();
        detection = GetComponent<Detection>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();

        //variables from main script
        walkStopRate = 0.05f;
        health = 20;
        walkSpeed = 2f;
        abletoMoveVector = new Vector2(-1, 0);
        killedEnemy = animator.gameObject;
        startcolor = spriteRenderer.color;

        isAlive = true;
        canMove = false;
    }

    private void FixedUpdate()
    {
        //checks if you are on the ground en hit a wall
        if (detection.isGrounded && detection.isOnwall || groundDetection.detectioncolls.Count == 0)
        {
            FlipDirection();
        }
        //lets them move if they are alive
        if (isMoving)
        {
            if (!gotTarget && isAlive)
            {
                rigidbody2D.velocity = new Vector2(walkSpeed * abletoMoveVector.x, rigidbody2D.velocity.y);
            }
            else if (gotTarget && isAlive)
            {
                rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0, walkStopRate), rigidbody2D.velocity.y);
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }

        //animation fade when dead
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("CrabDeath"))
        {
            canMove = false;

            timeElapsed += Time.deltaTime;
            newAlpha = (1 - timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startcolor.r, startcolor.g, startcolor.b, newAlpha);


            if (timeElapsed > fadeTime)
            {
                Destroy(killedEnemy);
            }
        }
        //locks the enemy from moving while his is attacking
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("CrabAttack"))
        {
            canMove = false;
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("CrabAttack") || !animator.GetCurrentAnimatorStateInfo(0).IsName("CrabDeath"))
        {
            canMove = true;
        }
    }
}
