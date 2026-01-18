using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMov : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float minForwardSpeed = 2f;
    public float groundDrag = 5f;
    public float airMultiplier = 0.4f;

    [Header("Slow Down")]
    public float slowMultiplier = 0.4f;
    public KeyCode slowKey = KeyCode.LeftControl;

    [Header("Jump")]
    public float jumpForce = 7f;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float currentForwardSpeed;

    Vector3 moveDirection;

    Rigidbody rb;
    CapsuleCollider col;

    bool readyToJump = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        rb.freezeRotation = true;
        currentForwardSpeed = moveSpeed;
    }

    void Update()
    {
        MyInput();
        SpeedControl();

        rb.drag = grounded ? groundDrag : 0f;

        if (grounded && !readyToJump)
            readyToJump = true;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Gestion du ralentissement
        if (Input.GetKey(slowKey))
            currentForwardSpeed = Mathf.Max(minForwardSpeed, moveSpeed * slowMultiplier);
        else
            currentForwardSpeed = moveSpeed;

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
        }
    }

    void MovePlayer()
    {
        Vector3 forwardMove = orientation.forward * currentForwardSpeed;
        Vector3 strafeMove = orientation.right * horizontalInput * currentForwardSpeed;

        moveDirection = (forwardMove + strafeMove).normalized;

        rb.AddForce(moveDirection * currentForwardSpeed * 10f, ForceMode.Force);

        if (!grounded)
        {
            rb.AddForce(moveDirection * currentForwardSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        float maxSpeed = currentForwardSpeed;

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // =====================
    // GROUND CHECK (COLLIDERS)
    // =====================

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) return;

        foreach (ContactPoint contact in collision.contacts)
        {
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
