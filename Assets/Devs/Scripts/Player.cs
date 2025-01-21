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
    private bool canMove = true;

    [Header("refrences")]
    private Rigidbody2D rb;

    [Header("health"), Range(0, 5)]
    private int health;
    private int maxHealth = 5;

    [Header("animations")]
    private Animator animator;
    private bool a_isMoving = false;

    [Header("other Scripts")]
    private Detection detection;

    [Header("iFrames")]
    private bool isInIFrames;
    private bool isAlive;
    private float timeSindsHit = 0;
    private float IframeTimer = 0.25f;

    //Properties
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
    public bool isLiving
    {
        get
        {
            return isAlive;
        }
        private set
        {
            isAlive = value;
            animator.SetBool("isAlive", value);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        detection = GetComponent<Detection>();

        isInIFrames = false;
        isAlive = true;

        health = maxHealth;
    }

    /// <summary>
    /// Check if you are not in the (playerattack, spawn or death) animation and lock movement if you are and let them move if you are not
    /// </summary>
    private void FixedUpdate()
    {
        //velocity en animaties voor movement
        rb.velocity = new Vector2(movementInput.x * moveSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("canMove", canMove);

        //check of the attack spawn of death state active is
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack_1")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
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
        //check if the button is pressed or held
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
            //check if the player is alive
            if (isAlive)
            {
                isMoving = movementInput != Vector2.zero;
                FacingDirection(movementInput);
            }
            else
            {
                canMove = false;
            }
        }

    }
    //checks the current direction you are facing
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
        //check is you ar jumping and play the jump animation
        if (context.started && detection.isGrounded && canMove)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        //plays the attack animations if the button is pressed
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

    private void OnApplicationQuit()
    {
        //voor reset logica
        inventory.container.Clear();
    }

    /// <summary>
    /// checks of you ar alive and if you are in iframes to take damage if you take damag play the Hit animation and if you die play the death animation
    /// </summary>
    /// <param name="Amount">the amount of damage you are going to take from enemies</param>
    public void TakeDamage(int Amount)
    {
        if (isAlive && !isInIFrames)
        {
            health -= Amount;
            animator.SetTrigger("Hit");
            isInIFrames = true;
        }
        if (health <= 0)
        {
            isAlive = false;
            animator.SetBool("isAlive", isAlive);
        }
    }

    //checks and sets up Iframes
    private void Update()
    {
        if (timeSindsHit > IframeTimer)
        {
            isInIFrames = false;
            timeSindsHit = 0;
        }
        timeSindsHit += Time.deltaTime;
    }
}
