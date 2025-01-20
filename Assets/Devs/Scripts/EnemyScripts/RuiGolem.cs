using UnityEngine;
public class RuiGolem : Enemy
{
   
    public void Start()
    {
        attackZone = GetComponentInChildren<DetectionZone>();
        animator = GetComponent<Animator>();
        detection = GetComponent<Detection>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();

        //variables from main script
        walkStopRate = 0.05f;
        health = 900;
        walkSpeed = 3f;
        abletoMoveVector = new Vector2(-1, 0);
        killedEnemy = animator.gameObject;
        startcolor = spriteRenderer.color;

        isLiving = true;
    }

    private void FixedUpdate()
    {
        if (detection.isGrounded && detection.isOnwall)
        {
            FlipDirection();
        }

        if (canMove)
        {
            if (!gotTarget && isLiving)
            {
                rigidbody2D.velocity = new Vector2(walkSpeed * abletoMoveVector.x, rigidbody2D.velocity.y);
            }
            else if (gotTarget && isLiving)
            {
                rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0, walkStopRate), rigidbody2D.velocity.y);
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Death"))
        {
            canMove = false;

            timeElapsed += Time.deltaTime;
            newAlpha = (1 - timeElapsed / fadeTime);
            spriteRenderer.color = new Color(startcolor.r, startcolor.g, startcolor.b,newAlpha);


            if (timeElapsed > fadeTime)
            {
                //Destroy(killedEnemy);
            }
        }
        else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Death"))
        {
            canMove = true;
        }
    }
}
