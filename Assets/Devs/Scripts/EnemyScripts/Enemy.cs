using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(Detection))]
public class Enemy : MonoBehaviour, IDamageable
{
    [Header("components")]
    public new Rigidbody2D rigidbody2D;

    [Header("Health")]
    public int health;

    [Header("movement")]
    public float walkSpeed;
    public Vector2 abletoMoveVector;

    [Header("detections")]
    public Detection detection; 
    private MoveDirections moveDirections;
    public DetectionZone attackZone;
    public float walkStopRate;

    [Header("animations")]
     public Animator animator;

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


    public enum MoveDirections { right, left }

    public virtual void TakeDamage(int Amount)
    {
        health -= Amount;
        if (health <= 0)
        {
            //plays death animation
            //chance to drop spell WIP;
            Destroy(gameObject);
        }
    }

    public  void FlipDirection()
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
