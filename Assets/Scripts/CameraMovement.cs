﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform cameraTarget;
    public float cameraSpeed;
    public bool enableHorizontalCameraMovement;
    public bool enableVerticalCameraMovement;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if (cameraTarget != null)
        {
            var newPos = Vector2.Lerp(transform.position,
            cameraTarget.position,
            Time.deltaTime * cameraSpeed);

            var vect3 = new Vector3(newPos.x, newPos.y, -10f);

            // Clamp camera position to make the character be in the left side of the screen
            var clampX = 0f;
            var clampY = 0f;
            if (enableHorizontalCameraMovement)
            {
                clampX = Mathf.Clamp(vect3.x, minX, maxX);
            }
            else
            {
                clampX = 0;
            }
            if (enableVerticalCameraMovement)
            {
                clampY = Mathf.Clamp(vect3.y, minY, maxY);
            }
            else
            {
                clampY = 0;
            }

            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }
}
