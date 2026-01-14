using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnOnTouch : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        other.transform.position = spawnPoint.position;
    }
}