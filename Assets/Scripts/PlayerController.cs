using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float jumpForce;
    public float moveSpeed;
    public float maxVelocity;
    public bool onGround;
    public float groundDist;
    public LayerMask groundMask;

    //Components
    Rigidbody2D rb;

    //Inputs
    public InputAction moveAction;
    public float moveVal;
    public InputAction jumpAction;
    bool jumpVal;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveVal = moveAction.ReadValue<float>();
        jumpVal = jumpAction.IsPressed();

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundDist, groundMask);
    }

    private void FixedUpdate()
    {
        if(jumpVal && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
        }

        rb.AddForce(Vector2.right * moveSpeed * moveVal * Time.deltaTime);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), rb.velocity.y);

        if(moveVal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
    }

    #region Enable and Disable Inputs

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    #endregion
}
