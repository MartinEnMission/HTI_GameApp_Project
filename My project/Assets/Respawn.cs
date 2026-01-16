using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnOnTouch : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = spawnPoint.position;
    }
}