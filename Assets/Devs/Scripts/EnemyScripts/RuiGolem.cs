using UnityEngine;
public class RuiGolem : Enemy
{
    private bool canMove = true;
    private bool gotTarget = false;

    public bool hasTarget
    {
        get { return gotTarget; }
        private set
        {

            gotTarget = value;
            animator.SetBool("HasTarget", value);
        }
    }
    public void Awake()
    {
        attackZone = GetComponentInChildren<DetectionZone>();
        animator = GetComponent<Animator>();
        detection = GetComponent<Detection>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        //variables from main script
        walkStopRate = 0.05f;
        health = 9;
        walkSpeed = 3f;
        abletoMoveVector = new Vector2(-1, 0);
    }


    private void FixedUpdate()
    {
        if (detection.isGrounded && detection.isOnwall)
        {
            FlipDirection();
        }
        if (!gotTarget)
        {
            rigidbody2D.velocity = new Vector2(walkSpeed * abletoMoveVector.x, rigidbody2D.velocity.y);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0, walkStopRate), rigidbody2D.velocity.y);
        }
    }


    private void Update()
    {
        hasTarget = attackZone.detectioncolls.Count > 0;
    }
}
