using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBoxBug : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;

    private float width;
    private float height;

    public bool enableMovement = true;
    public bool isDead = false;
    public int moveDistance;
    public float totalMovement = 100;
    public float moveSpeed = 5f;
    public bool isMovingRight = false;
    void Start()
    {
        moveDistance = 0;
    }

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        width = GetComponent<Collider2D>().bounds.extents.x + 0.1f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.1f;
    }

    void FixedUpdate()
    {
        // animator.SetBool("IsDead", isDead);
        if (!isDead)
        {
            if(enableMovement)
            {
                if (moveDistance <= totalMovement)
                {
                    if (!isMovingRight)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * moveSpeed;
                    }
                    else
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * moveSpeed;
                    }
                    moveDistance++;
                }
                else
                {
                    isMovingRight = !isMovingRight;
                    moveDistance = 0;
                }
            }
        }
        else
        {
            animator.SetBool("IsDead", isDead);
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        animator.SetBool("IsDead", isDead);
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
