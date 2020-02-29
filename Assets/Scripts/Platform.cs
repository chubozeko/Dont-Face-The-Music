using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 2f;
    [Header("Vertical Movement")]
    public bool enableVerticalMovement = false;
    // public bool moveVertically;
    public int verticalMoveDistance;
    public float totalVerticalMovement = 50;
    public bool isMovingDown = false;
    [Header("Horizontal Movement")]
    public bool enableHorizontalMovement = false;
    // public bool moveHorizontally;
    public int horizontalMoveDistance;
    public float totalHorizontalMovement = 50;
    public bool isMovingLeft = false;

    void Start()
    {
        verticalMoveDistance = 0;
        // moveVertically = false;
        horizontalMoveDistance = 0;
        // moveHorizontally = false;
    }

    void FixedUpdate()
    {
        // Vertical Movement
        if(enableVerticalMovement)
        {
            if (verticalMoveDistance <= totalVerticalMovement)
            {
                if (isMovingDown)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * movementSpeed;
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * movementSpeed;
                }
                verticalMoveDistance++;
            }
            else
            {
                isMovingDown = !isMovingDown;
                verticalMoveDistance = 0;
            }
        }

        // Horizontal Movement
        if(enableHorizontalMovement)
        {
            if (horizontalMoveDistance <= totalHorizontalMovement)
            {
                if (isMovingLeft)
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0) * movementSpeed;
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0) * movementSpeed;
                }
                horizontalMoveDistance++;
            }
            else
            {
                isMovingLeft = !isMovingLeft;
                horizontalMoveDistance = 0;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
