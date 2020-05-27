using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Component Properties")]
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    private float width;
    private float height;

    [Header("Movement")]
    [Range(1f, 8f)]
    public float speed = 5f;
    public bool isFacingRight = true;

    [Header("Jumping")]
    [Range(1f, 8f)]
    public float jumpSpeed = 5f;
    public float wallJumpY = 10f;
    public bool isJumping = false;
    private float jumpButtonPressTime;
    private float maxJumpTime = 0.2f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Point System")]
    public Text scoreLine;
    public Text livesRemaining;
    private int score = 0;
    private int lives = 3;
    
    private float rayCastLength = 0.005f;

    void Start()
    {
        score = 0;
        lives = 3;
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
        float horzMove = Input.GetAxisRaw("Horizontal");
        Vector2 vect = rb.velocity;
        rb.velocity = new Vector2(horzMove * speed, vect.y);

        if (IsWallOnLeftOrRight() && !IsOnGround() && horzMove == 1)
        {
            rb.velocity = new Vector2(-GetWallDirection() * speed * -.75f, wallJumpY);
        }

        animator.SetFloat("Speed", Mathf.Abs(horzMove));

        if (horzMove > 0 && !isFacingRight)
        {
            FlipPlayer();
        }
        else if (horzMove < 0 && isFacingRight)
        {
            FlipPlayer();
        }

        float vertMove = Input.GetAxisRaw("Vertical");

        if (IsOnGround() && isJumping == false)
        {
            if (vertMove > 0f)
            {
                isJumping = true;
                //BetterJump();
            }
        }

        if (jumpButtonPressTime > maxJumpTime)
        {
            vertMove = 0f;
        }

        if (isJumping && jumpButtonPressTime < maxJumpTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            //rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            BetterJump();
        }

        if (vertMove >= 1f)
        {
            jumpButtonPressTime += Time.deltaTime;
        }
        else
        {
            isJumping = false;
            jumpButtonPressTime = 0f;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        animator.SetBool("IsJumping", isJumping);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Vinyl")
        {
            score += 100;
            //Debug.Log("Vinyl");
            AudioManager.Instance.soundEffectAudio.PlayOneShot(AudioManager.Instance.levelUpSound);
            Destroy(collision.gameObject);
        }

        if (collision.tag == "SecretPod")
        {
            score -= 10;
            lives -= 1;
            collision.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            // Debug.Log("Secret Hit");
            AudioManager.Instance.soundEffectAudio.PlayOneShot(AudioManager.Instance.lostLifeSound);
        }

        if (collision.tag == "BoomBoxBug")
        {
            AudioManager.Instance.soundEffectAudio.PlayOneShot(AudioManager.Instance.lostLifeSound);
        }

        scoreLine.text = "Score: " + score;
        livesRemaining.text = "Lives: " + lives;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            score += 500;
            Debug.Log("Level Complete!");
            //AudioManager.Instance.soundEffectAudio.PlayOneShot(AudioManager.Instance.levelUpSound);
            //Destroy(collision.gameObject);
        }
    }

    void FlipPlayer()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public bool IsOnGround()
    {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(
            transform.position.x,
            transform.position.y - height),
            -Vector2.up,
            rayCastLength);
        bool groundCheck2 = Physics2D.Raycast(new Vector2(
            transform.position.x + (width - 0.2f),
            transform.position.y - height),
            -Vector2.up,
            rayCastLength);
        bool groundCheck3 = Physics2D.Raycast(new Vector2(
            transform.position.x - (width - 0.2f),
            transform.position.y - height),
            -Vector2.up,
            rayCastLength);

        if (groundCheck1 || groundCheck2 || groundCheck3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsWallOnLeft()
    {
        bool boolVal = false;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - width,
            transform.position.y),
            -Vector2.right,
            rayCastLength);

        if(hit != false)
        {
            if (hit.collider.gameObject.tag != "Ground")
            {
                boolVal = true;
            }
            else
            {
                boolVal = false;
            }
        }
        return boolVal;
        /*
        return Physics2D.Raycast(new Vector2(transform.position.x - width,
            transform.position.y),
            -Vector2.right,
            rayCastLength);
        */
    }

    public bool IsWallOnRight()
    {
        bool boolVal = false;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + width,
            transform.position.y),
            Vector2.right,
            rayCastLength);

        if (hit != false)
        {
            if (hit.collider.gameObject.tag != "Ground")
            {
                boolVal = true;
            }
            else
            {
                boolVal = false;
            }
        }
        return boolVal;
        /*
        return Physics2D.Raycast(new Vector2(transform.position.x + width,
            transform.position.y),
            Vector2.right,
            rayCastLength);
        */
    }

    public bool IsWallOnLeftOrRight()
    {
        if (IsWallOnLeft() || IsWallOnRight())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetWallDirection()
    {
        if (IsWallOnLeft())
        {
            return -1;
        }
        else if (IsWallOnRight())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void BetterJump()
    {
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void ChangeScore(int points)
    {
        score += points;
    }

    public void ChangeLives(int life)
    {
        lives += life;
    }
}
