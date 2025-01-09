using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour,IDamageable
{
    [Header("inventory")]
    public InventoryObjects inventory;

    [Header("movement"),Range(0,15)]
    private float moveSpeed = 2.5f;
    private float forwardMovement;
    private float jumpHeight = 10;

    [Header("grounding")]
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Transform groundCheck;

    [Header("refrences")]
    private Rigidbody2D rb;

    [Header("health"),Range(0,5)]
    private int health;
    private int maxHealth = 5;

    [Header("damage")]
    private int damageDone = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            forwardMovement = context.ReadValue<Vector2>().x;
        }
    }

    private void Update()
    {
        rb.velocity = new Vector2(forwardMovement * moveSpeed,rb.velocity.y);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed && WhatIsGround())
        {
            rb.velocity += Vector2.up * jumpHeight;
        }
    }

    private bool WhatIsGround()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.65f, 0.01f), CapsuleDirection2D.Horizontal,0,groundlayer);
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
            } else
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
        inventory.container.Clear();
    }

    public void TakeDamage(int Amount)
    {
        health -= Amount;
    }
}
