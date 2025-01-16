using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable
{
    [Header("inventory")]
    public InventoryObjects inventory;

    [Header("movement"), Range(0, 15)]
    private Vector2 movementInput;
    private float jumpHeight = 10f;
    private float movementSpeed = 5f;
    private bool isLookingRight = true;
    [SerializeField] private bool canMove = true;

    //Get and set variables for movement
    public float moveSpeed
    {
        get
        {
            if (canMove)
            {
                if (isMoving && !detection.isOnwall)
                {
                    return movementSpeed;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }
    }
    public bool isFacingRight
    {
        get { return isLookingRight; }
        private set
        {
            if (isLookingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            isLookingRight = value;
        }
    }
    public bool isMoving
    {
        get
        {
            return a_isMoving;
        }
        private set
        {
            a_isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }


    [Header("refrences")]
    private Rigidbody2D rb;

    [Header("health"), Range(0, 5)]
    private int health;
    private int maxHealth = 5;

    [Header("damage")]
    private int damageDone = 1;

    [Header("animations")]
    private Animator animator;
    private bool a_isMoving = false;

    [Header("other Scripts")]
    private Detection detection;

    [Header("iFrames")]
    private bool isInIFrames;
    private bool isAlive;
    private float timeSindsHit = 0;
    public float IframeTimer = 0.25f; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        detection = GetComponent<Detection>();

        isInIFrames = false;
        isAlive = true;

        health = maxHealth;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("CanMove", canMove);



        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack_1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
            isMoving = movementInput != Vector2.zero;
            FacingDirection(movementInput);
        }
    }

    private void FacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && detection.isGrounded && canMove)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            // check if there is room for new spells...
            if (inventory.AddSpells(item.PickUp))
            {
                // player feedback, success
            }
            else
            {
                // player feedback, no more room
            }
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("no item found");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable obj = collision.gameObject.GetComponent<IDamageable>();

        if (obj != null)
        {
            obj.TakeDamage(damageDone);
        }
    }

    private void OnApplicationQuit()
    {
        //voor rest logica
        inventory.container.Clear();
    }

    public void TakeDamage(int Amount)
    {
        if (isAlive && !isInIFrames)
        {
            health -= Amount;
            animator.SetTrigger("Hit");
            isInIFrames = true;
        }
    }

    private void Update()
    {
        if(timeSindsHit > IframeTimer)
        {
            isInIFrames = false;
            timeSindsHit = 0;
        }
        timeSindsHit += Time.deltaTime;
        TakeDamage(1);
    }
}
