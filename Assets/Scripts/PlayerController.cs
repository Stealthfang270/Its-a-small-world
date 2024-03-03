using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float jumpForce;
    public float moveSpeed;
    public float maxVelocity;
    public bool onGround;
    public float groundDist;
    public LayerMask groundMask;
    public Vector2 respawnLocation;
    public GameObject square;
    public Camera cam;
    public AudioSource src;

    public Animator animator;

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
        EventManager.playerDeath.AddListener(Perish);
        EventManager.setPlayerRespawnLocation.AddListener(SetRespawnLocation);
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene("MainMenu"); }
        moveVal = moveAction.ReadValue<float>();
        jumpVal = jumpAction.IsPressed();

        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundDist, groundMask);

        animator.SetBool("onGround", onGround);

        if (moveAction.WasPressedThisFrame() && moveVal < 0)
        {
            square.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveAction.WasPressedThisFrame() && moveVal > 0)
        {
            square.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void FixedUpdate()
    {
        if (jumpVal && onGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce);
        }

        rb.AddForce(Vector2.right * moveSpeed * moveVal * Time.deltaTime);

        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity), rb.velocity.y);

        if (moveVal == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x / 1.1f, rb.velocity.y);
        }
    }

    void Perish()
    {
        rb.velocity = Vector2.zero;
        transform.position = respawnLocation;
        src.Play();
    }

    void SetRespawnLocation(Vector2 pos)
    {
        respawnLocation = pos;
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
