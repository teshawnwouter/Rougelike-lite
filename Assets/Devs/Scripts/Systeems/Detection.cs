using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class Detection : MonoBehaviour
{
    [Header("animations")]
    private Animator animator;

    [Header("colliders")]
    private CapsuleCollider2D touchingcollider;
    public  ContactFilter2D collisionFilter;

    [Header("raycastHits")]
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    [Header("Distancecheck")]
    private float groundDist = 0.05f;
    private float ceilingDist = 0.05f;
    private float wallDist = 0.2f;

    [Header("bools for checking")]
    public bool isOnGround = true;
    private bool isOnWall;
    private bool isOnCeiling;

    //for left and right wal check
    private Vector2 wallchecking => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    //get and set for if touching
    /// <summary>
    /// Kijkt of je de grond aan raakt en zet een bool aan in de speler en animator
    /// </summary>
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
        //colliders die een casts sturen om te kijken of je speler een muur het plafon of the grond aan raakt
        isGrounded = touchingcollider.Cast(Vector2.down, collisionFilter, groundHits, groundDist) > 0;
        isOnwall = touchingcollider.Cast(wallchecking, collisionFilter, wallHits, wallDist) > 0;
        isOnCeiling = touchingcollider.Cast(Vector2.up, collisionFilter, ceilingHits, ceilingDist) > 0;
    }
}