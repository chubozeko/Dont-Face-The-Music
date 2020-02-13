using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public int moveDistance;
    public float totalMovement = 50;
    public float moveSpeed = 3f;
    public bool isMovingDown = false;
    private Rigidbody2D rb;

    void Start()
    {
        moveDistance = 0;
    }

    void FixedUpdate()
    {
        if(moveDistance <= totalMovement)
        {
            if (isMovingDown)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * moveSpeed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * moveSpeed;
            }
            moveDistance++;
        }
        else
        {
            isMovingDown = !isMovingDown;
            moveDistance = 0;
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
