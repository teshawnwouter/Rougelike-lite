using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Bat : MonoBehaviour, IDamageable
{
    private DetectionZone attackZone;
    private float flightSpeed = 3;
    private float destenationReached = 0;

    private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    bool gotTarget;

    public bool isAlive;
    public bool canMove;

    private int health = 30;

    private int waypointIndex;
    public GameObject target;

    public float fadeTime = 0.5f;
    public float timeElapsed = 0f;
    public SpriteRenderer spriteRenderer;
    public GameObject killedEnemy;
    public Color startcolor;
    public float newAlpha;

    private Vector2 offset;
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

    public UnityEvent OnLootDrop; 

    public bool hasTarget
    {
        get { return gotTarget; }
        private set
        {
            gotTarget = value;
            animator.SetBool("HasTarget", value);
            killedEnemy = animator.gameObject;
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackZone = GetComponentInChildren<DetectionZone>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnDisable()
    {
        OnLootDrop.Invoke();
    }
    public void LootDrop()
    {
        FindFirstObjectByType<LootList>().DroppedTheItem(transform.position);
    }

    private void Update()
    {
        hasTarget = attackZone.detectioncolls.Count > 0;

        animator.SetBool("isMoving", true);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("BatDeath"))
        {
            timeElapsed += Time.deltaTime;
            newAlpha = (1 - timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startcolor.r, startcolor.g, startcolor.b, newAlpha);


            if (timeElapsed > fadeTime)
            {
                Destroy(killedEnemy);
            }
        }
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
        Vector2 directionWayPoint = (target.transform.position - transform.position);

        float dist = Vector2.Distance(target.transform.position, transform.position);

        if(transform.localScale.x < 0)
        {
            offset = new Vector2(0.5f,0);
        }
        else
        {
            offset = new Vector2(-0.5f,0);
        }

        rb.velocity = directionWayPoint + offset * flightSpeed;
        UpDateDirection();

        if (dist < destenationReached)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void UpDateDirection()
    {
        Vector2 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector2(-1 * locScale.x, locScale.y);
            }
        }
        else
        {
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector2(-1 * locScale.x, locScale.y);
            }
        }
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
            
            canMove = false;
            isLiving = false;
        }
    }
}
