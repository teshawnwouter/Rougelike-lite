using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D touchingcollider;
    public  ContactFilter2D collisionFilter;

    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private float groundDist = 0.01f;
    private float ceilingDist = 0.05f;
    private float wallDist = 0.2f;

    [SerializeField] private bool isOnGround = true;
    private bool isOnWall;
    private bool isOnCeiling;
    private Vector2 wallchecking => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool isGrounded { get { return isOnGround; } private set 
        { 
            isOnGround = value;
            animator.SetBool("isGrounded", value);
        }
    }

    public bool isOnwall
    {
        get { return isOnWall; }
        private set
        {
            isOnWall = value;
            animator.SetBool("isOnWall", value);
        }
    }
    public bool isTouchingCeiling
    {
        get { return isOnCeiling; }
        private set
        {
            isOnCeiling = value;
            animator.SetBool("isOnCeiling", value);
        }
    }

    private void Awake()
    {
        touchingcollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
     
        isGrounded = touchingcollider.Cast(Vector2.down, collisionFilter, groundHits, groundDist) > 0;
        isOnwall = touchingcollider.Cast(wallchecking, collisionFilter, wallHits, wallDist) > 0;
        isOnCeiling = touchingcollider.Cast(Vector2.up, collisionFilter, ceilingHits, groundDist) > 0;
    }
}