using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bat : MonoBehaviour,IDamageable
{
    private DetectionZone attackZone;
    private float flightSpeed = 2;
    private float destenationReached = 0;

    private Rigidbody2D rb;
    Animator animator;
    bool gotTarget;

    public bool isAlive;
    public bool canMove;

    private int health = 3;

    private int waypointIndex;
    public Transform nextWaypoint;
    public bool isMoving
    {
        get { return canMove; }
        set
        {
            canMove = value;
            animator.SetBool("isMoving", value);
        }
    }

    public bool isLiving
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            animator.SetBool("IsAlive", value);
        }
    }


    public bool hasTarget
    {
        get { return gotTarget; }
        private set
        {
            gotTarget = value;
            animator.SetBool("HasTarget", value);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackZone = GetComponentInChildren<DetectionZone>();
    }
   
    private void Update()
    { 
        hasTarget = attackZone.detectioncolls.Count > 0;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (isAlive)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        
    }

    private void Flight()
    {
        Vector2 directionWayPoint = (nextWaypoint.position - transform.position);

        float dist = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionWayPoint * flightSpeed;
        // UpDateDirection();

        if(dist < destenationReached)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void UpDateDirection()
    {
        
    }

    public void TakeDamage(int Amount)
    {

        if (isAlive)
        {
            health -= Amount;
            animator.SetTrigger("Hit");
        }
        if (health <= 0)
        {
            Death();
        }  
    }
    public void Death()
    {
        canMove = false;
        isLiving = false;
        if (!isLiving)
        {
            FindFirstObjectByType<LootList>().DroppedTheItem(transform.position);
        }
    }
}
