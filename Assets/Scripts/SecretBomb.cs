using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBomb : MonoBehaviour
{
    [Header ("Position")]
    public float minX = -8.0f;
    public float maxX = 8.0f;
    public float minY = -3.0f;
    public float maxY = 5.0f;
    [Header("Raycast Variables")]
    public float rayCastLength = 4;
    private LayerMask layerMask;
    private Vector2 startPoint1 = Vector2.zero;
    private Vector2 pivotPoint1 = Vector2.zero;
    private Vector2 startPoint2 = Vector2.zero;
    private Vector2 pivotPoint2 = Vector2.zero;
    private Vector2 startPoint3 = Vector2.zero;
    private Vector2 pivotPoint3 = Vector2.zero;
    private Vector2 startPoint4 = Vector2.zero;
    private Vector2 pivotPoint4 = Vector2.zero;
    private Vector2 startPoint5 = Vector2.zero;
    private Vector2 pivotPoint5 = Vector2.zero;
    private Vector2 startPoint6 = Vector2.zero;
    private Vector2 pivotPoint6 = Vector2.zero;
    private Vector2 startPoint7 = Vector2.zero;
    private Vector2 pivotPoint7 = Vector2.zero;
    private Vector2 startPoint8 = Vector2.zero;
    private Vector2 pivotPoint8 = Vector2.zero;
    public float angle1 = -180.0f;
    public float angle2 = -90.0f;
    public float angle3 = 0.0f;
    public float angle4 = 90.0f;
    public float angle5 = -135.0f;
    public float angle6 = -45.0f;
    public float angle7 = 45.0f;
    public float angle8 = 135.0f;

    void Start()
    {
        //transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        layerMask = LayerMask.GetMask("Player");
    }

    void FixedUpdate()
    {

    }

    public float CheckRaycast()
    {
        //rayCastLength = rangeNr;
        // Update starting ray point.
        startPoint1 = (Vector2)transform.position + pivotPoint1;
        startPoint2 = (Vector2)transform.position + pivotPoint2;
        startPoint3 = (Vector2)transform.position + pivotPoint3;
        startPoint4 = (Vector2)transform.position + pivotPoint4;
        startPoint5 = (Vector2)transform.position + pivotPoint5;
        startPoint6 = (Vector2)transform.position + pivotPoint6;
        startPoint7 = (Vector2)transform.position + pivotPoint7;
        startPoint8 = (Vector2)transform.position + pivotPoint8;

        Vector2 angleDirection1 = new Vector2(Mathf.Cos(angle1 * Mathf.Deg2Rad), Mathf.Sin(angle1 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection2 = new Vector2(Mathf.Cos(angle2 * Mathf.Deg2Rad), Mathf.Sin(angle2 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection3 = new Vector2(Mathf.Cos(angle3 * Mathf.Deg2Rad), Mathf.Sin(angle3 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection4 = new Vector2(Mathf.Cos(angle4 * Mathf.Deg2Rad), Mathf.Sin(angle4 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection5 = new Vector2(Mathf.Cos(angle5 * Mathf.Deg2Rad), Mathf.Sin(angle5 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection6 = new Vector2(Mathf.Cos(angle6 * Mathf.Deg2Rad), Mathf.Sin(angle6 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection7 = new Vector2(Mathf.Cos(angle7 * Mathf.Deg2Rad), Mathf.Sin(angle7 * Mathf.Deg2Rad)).normalized;
        Vector2 angleDirection8 = new Vector2(Mathf.Cos(angle8 * Mathf.Deg2Rad), Mathf.Sin(angle8 * Mathf.Deg2Rad)).normalized;

        RaycastHit2D hit1 = Physics2D.Raycast(
            startPoint1,
            angleDirection1,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint1, angleDirection1 * rayCastLength, Color.red);


        RaycastHit2D hit2 = Physics2D.Raycast(
            startPoint2,
            angleDirection2,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint2, angleDirection2 * rayCastLength, Color.red);

        RaycastHit2D hit3 = Physics2D.Raycast(
            startPoint3,
            angleDirection3,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint3, angleDirection3 * rayCastLength, Color.red);

        RaycastHit2D hit4 = Physics2D.Raycast(
            startPoint4,
            angleDirection4,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint4, angleDirection4 * rayCastLength, Color.red);

        RaycastHit2D hit5 = Physics2D.Raycast(
            startPoint5,
            angleDirection5,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint5, angleDirection5 * rayCastLength, Color.red);

        RaycastHit2D hit6 = Physics2D.Raycast(
            startPoint6,
            angleDirection6,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint6, angleDirection6 * rayCastLength, Color.red);

        RaycastHit2D hit7 = Physics2D.Raycast(
            startPoint7,
            angleDirection7,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint7, angleDirection7 * rayCastLength, Color.red);

        RaycastHit2D hit8 = Physics2D.Raycast(
            startPoint8,
            angleDirection8,
            rayCastLength,
            layerMask);
        Debug.DrawRay(startPoint8, angleDirection8 * rayCastLength, Color.red);

        angle1 += 2.0f;
        angle2 += 2.0f;
        angle3 += 2.0f;
        angle4 += 2.0f;
        angle5 += 2.0f;
        angle6 += 2.0f;
        angle7 += 2.0f;
        angle8 += 2.0f;

        RaycastHit2D[] hits = {hit1, hit2, hit3, hit4, hit5, hit6, hit7, hit8 };
        return CheckHitDistance(hits);
    }

    public float CheckHitDistance(RaycastHit2D[] hits)
    {
        foreach (RaycastHit2D hit1 in hits)
        {
            if (hit1.collider != null)
            {
                return Remap(hit1.distance, rayCastLength, 0.0f, 0.0f, 1.0f);
            }
        }
        return 0;
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT");
    }

}
