using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("inventory")]
    public InventoryObjects inventory;

    [Header("movement")]
    private float moveSpeed = 2.5f;
    private float forwardMovement;
    private float jumpHeight = 10;

    [Header("grounding")]
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private Transform groundCheck;

    [Header("refrences")]
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        forwardMovement = context.ReadValue<Vector2>().x;
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
            inventory.addSpell(item.PickUp);
            Destroy(collision.gameObject);
        }
        else
        {
            Debug.Log("no item found");
        }
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }
}