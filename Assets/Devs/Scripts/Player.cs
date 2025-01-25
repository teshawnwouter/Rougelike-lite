using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamageable
{
    [Header("inventory")]
    public InventoryObjects inventory;

    [Header("movement"), Range(0, 15)]
    private Vector2 movementInput;
    private float jumpHeight = 10f;
    private float movementSpeed = 5f;
    private float plungSpeed = 1.5f;
    private bool isLookingRight = true;
    private bool canMove = true;
    private bool canDoubleJump = true;
    private bool canPlunge = false;

    [Header("refrences")]
    private Rigidbody2D rb;

    [Header("health")]
    public int health;
    public int maxHealth = 100;

    [Header("animations")]
    public Animator animator;
    private bool a_isMoving = false;

    [Header("other Scripts")]
    private Detection detection;
    private Spells currentSpell;
    public HealthBarSlider healthBarSlider;

    [Header("iFrames")]
    private bool isInIFrames;
    public bool isAlive;
    private float timeSindsHit = 0;
    private float IframeTimer = 0.25f;

    [Header("spells")]
    private int selectedSpell;
    private SpellCasting spellCasting;
    public Image spellcontainer;
    public List<Sprite> sprites;

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
        }
    }
    public bool isGoingTojumpAgain
    {
        get { return canDoubleJump; }
        private set
        {
            canDoubleJump = value;
        }
    }
    public bool isSumersalting
    {
        get { return canPlunge; }
        private set
        {
            canPlunge = value;
        }
    }

    public UnityEvent Ondeath;

    private void Awake()
    {
        healthBarSlider = FindFirstObjectByType<HealthBarSlider>();
        spellCasting = GetComponent<SpellCasting>();
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        detection = GetComponent<Detection>();

        isInIFrames = false;
        isAlive = true;
        health = maxHealth;

        healthBarSlider.SetMaxHealth(maxHealth);
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
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack_2")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack_3")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Spawn")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerDeath"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAir_Attack_1")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAir_Attack_2"))
        {
            rb.velocity = new Vector2(rb.velocity.x / 2, 0);
            animator.ResetTrigger("Jump");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_plungeLoop"))
        {
            rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y - plungSpeed);
            animator.ResetTrigger("Jump");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_Slam"))
        {
            animator.ResetTrigger("Jump");
            animator.ResetTrigger("Attack");
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sumersalt"))
        {
            isSumersalting = true;
        }

    }

    //checks and sets up Iframes
    private void Update()
    {
        healthBarSlider.SetHealth(health);
        if (timeSindsHit > IframeTimer)
        {
            isInIFrames = false;
            timeSindsHit = 0;
        }
        timeSindsHit += Time.deltaTime;

        if (detection.isGrounded)
        {
            canDoubleJump = true;
            canPlunge = false;
        }
        CheckDoubleJump();
        GoingToPlunge();

        if (inventory.container != null)
        {
            CheckingSpellIndex();
        }
    }

    public void ResteInventory()
    {
        inventory.container.Clear();
        animator.SetBool("isAlive", isAlive);
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
        else if (context.started && !detection.isGrounded && isGoingTojumpAgain && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            canDoubleJump = false;
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
    // waneer je de spell cast loopt hij door je inventory roept hij een functie van de spellcasting script en returnt naar het begin
    public void OnSpellCast(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("CastSpell");
            for (int i = 0; i < inventory.container.Count; i++)
            {
                if (selectedSpell == inventory.container.IndexOf(spellCasting.fireBall, i))
                {
                    spellCasting.FireBall();
                    inventory.container.RemoveAt(i);
                    return;
                }
                if (selectedSpell == inventory.container.IndexOf(spellCasting.iceShard, i))
                {
                    spellcontainer.sprite = sprites[1];
                    spellCasting.IceShard();
                    inventory.container.RemoveAt(i);
                    return;
                }
                if (selectedSpell == inventory.container.IndexOf(spellCasting.auraBlast, i))
                {
                    spellcontainer.sprite = sprites[2];
                    spellCasting.AuraBurst();
                    inventory.container.RemoveAt(i);
                    return;
                }
                if (selectedSpell == inventory.container.IndexOf(spellCasting.healing, i))
                {
                    spellcontainer.sprite = sprites[3];
                    spellCasting.Healing();
                    inventory.container.RemoveAt(i);
                    return;
                }
                if (selectedSpell == inventory.container.IndexOf(spellCasting.explosion, i))
                {
                    spellcontainer.sprite = sprites[4];
                    spellCasting.Explosion();
                    inventory.container.RemoveAt(i);
                    return;
                }
            }
        }
    }

    //hij gaat voorbij een switch om tekijken wat de selected spell is en als de selected spell index grooter  of gelijk is aan de inventory index gaat hij na 0
    public void OnSpellSwitch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventory.container.Count != 0)
            {
                selectedSpell++;
                if (selectedSpell >= inventory.container.Count)
                {
                    selectedSpell = 0;
                }
            }
        }
    }

    //loops through the inventory and places the sprite depending on what spell is in the inventory
    private void CheckingSpellIndex()
    {
        for (int i = 0; i < inventory.container.Count; i++)
        {
            if (selectedSpell == inventory.container.IndexOf(spellCasting.fireBall, i))
            {
                spellcontainer.sprite = sprites[0];
            }
            if (selectedSpell == inventory.container.IndexOf(spellCasting.auraBlast, i))
            {
                spellcontainer.sprite = sprites[1];
            }
            if (selectedSpell == inventory.container.IndexOf(spellCasting.healing, i))
            {
                spellcontainer.sprite = sprites[2];
            }
            if (selectedSpell == inventory.container.IndexOf(spellCasting.explosion, i))
            {
                spellcontainer.sprite = sprites[3];
            }
            if (selectedSpell == inventory.container.IndexOf(spellCasting.iceShard, i))
            {
                spellcontainer.sprite = sprites[4];
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PickUps"))
        {
            AddToInventory item = collision.gameObject.GetComponent<AddToInventory>();
            if (item != null)
            {
                // check if there is room for new spells...
                if (inventory.AddSpells(item.spells))
                {
                    // player feedback, success
                }
                //else
                //{
                //    // player feedback, no more room
                //}
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("no item found");
            }
        }
    }

    /// <summary>
    /// checks of you ar alive and if you are in iframes to take damage if you take damag play the Hit animation and if you die play the death animation
    /// </summary>
    /// <param name="Amount">the amount of damage you are going to take from enemies</param>
    public void TakeDamage(int Amount)
    {
        if (isLiving && !isInIFrames)
        {

            health -= Amount;
            animator.SetTrigger("Hit");
            isInIFrames = true;
        }

        if (health <= 0)
        {
            isAlive = false;
            Ondeath.Invoke();
        }
    }

    private void CheckDoubleJump()
    {
        if (!canDoubleJump)
        {
            animator.SetBool("isGoingToSumersalt", false);
        }
        else
        {
            animator.SetBool("isGoingToSumersalt", true);
        }
    }

    private void GoingToPlunge()
    {
        if (isSumersalting)
        {
            animator.SetBool("isGoingToSlam", true);
        }
        else
        {
            animator.SetBool("isGoingToSlam", false);
        }
    }
}
