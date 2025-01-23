using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Detection))]
public class Enemy : MonoBehaviour, IDamageable
{
    //makes them move left or right
    public enum MoveDirections { right, left }

    [Header("components")]
    public new Rigidbody2D rigidbody2D;

    [Header("Health"), Range(0, 1000)]
    public int health;
    public bool isAlive;

    [Header("movement")]
    public float walkSpeed;
    public Vector2 abletoMoveVector;
    public bool canMove;

    [Header("detections")]
    public Detection detection;
    private MoveDirections moveDirections;
    public DetectionZone attackZone;
    public bool gotTarget = false;
    public float walkStopRate;

    [Header("animations")]
    public Animator animator;
    public float fadeTime = 0.5f;
    public float timeElapsed = 0f;
    public SpriteRenderer spriteRenderer;
    public GameObject killedEnemy;
    public Color startcolor;
    public float newAlpha;   

    //Properties
    public bool isMoving
    {
        get { return canMove; }
        set
        {
            canMove = value;
            animator.SetBool("isMoving", value);
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
    public MoveDirections moveDir
    {
        get { return moveDirections; }
        set
        {
            if (moveDirections != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (moveDirections == MoveDirections.right)
                {
                    abletoMoveVector = Vector2.right;
                }
                else if (moveDirections == MoveDirections.left)
                {
                    abletoMoveVector = Vector2.left;
                }
            }
            moveDirections = value;
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

    private void Update()
    {
        hasTarget = attackZone.detectioncolls.Count > 0;
    }

    private void Start()
    {
    }
    public virtual void TakeDamage(int Amount)
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

    public void FlipDirection()
    {
        if (moveDir == MoveDirections.right)
        {
            moveDir = MoveDirections.left;
        }
        else if (moveDir == MoveDirections.left)
        {
            moveDir = MoveDirections.right;
        }
    }

    
}
