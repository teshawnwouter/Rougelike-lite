using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("inventory")]
    public InventoryObjects inventory;

    [Header("movement")]
    private Vector2 moveInputsVectors = Vector2.zero;
    [SerializeField] private float moveSpeed = 2.5f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if ((context.performed))
        {
            moveInputsVectors = context.ReadValue<Vector2>();
            rb.velocity = moveInputsVectors * moveSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();
        if ((item) != null)
        {
            inventory.addSpell(item.PickUp, 1);
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
