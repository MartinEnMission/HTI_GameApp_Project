using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // degrés par seconde

    void Update()
    {
        transform.Rotate( 0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
}

