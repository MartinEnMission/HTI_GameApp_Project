using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMov : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    [Header("Jump")]
    public float jumpForce;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    CapsuleCollider col;

    bool readyToJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;
    }

    void Update()
    {
        MyInput();
        SpeedControl();

        // Drag
        rb.drag = grounded ? groundDrag : 0f;

        // Reset jump quand on touche le sol
        if (grounded && !readyToJump)
        {
            readyToJump = true;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
        }
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        foreach (ContactPoint contact in collision.contacts)
        {
            // Vérifie que le contact vient bien du dessous
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5f)
            {
                grounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;
        grounded = false;
    }
}
