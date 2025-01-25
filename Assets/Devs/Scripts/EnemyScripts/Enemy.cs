using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(Detection))]
public class Enemy : MonoBehaviour, IDamageable
{
    //makes them move left or right
    public enum MoveDirections { right, left }

    [Header("components")]
    public new Rigidbody2D rigidbody2D;

    [Header("Health")]
    public int health;
    public bool isAlive;

    [Header("movement")]
    public float walkSpeed;
    public Vector2 abletoMoveVector;
    public bool canMove;

    [Header("detections")]
    public Detection detection;
    [SerializeField] private MoveDirections moveDirections;
    public DetectionZone groundDetection;
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

    public UnityEvent OnEnemyDeath;

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

    private void OnDisable()
    {
        OnEnemyDeath.Invoke();
    }

    private void Update()
    {
        hasTarget = attackZone.detectioncolls.Count > 0;
    }

    public void LootDrop()
    {
        FindFirstObjectByType<LootList>().DroppedTheItem(transform.position);
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
            isLiving = false;
            canMove = false;
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
