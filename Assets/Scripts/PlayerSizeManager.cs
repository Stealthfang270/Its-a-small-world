using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSizeManager : MonoBehaviour
{
    public Vector3 bigScale;
    public Vector3 smallScale;

    Vector3 targetScale;
    Vector3 oldScale;
    public float scaleTime;
    float lerpFloat;
    public float scaleUpDist;
    public LayerMask groundMask;

    public bool isBig = true;

    Camera cam;

    public InputAction switchAction;

    //Big Player Stats
    public float bigJumpForce;
    public float bigMoveSpeed;
    public float bigMaxVelocity;
    public float bigMaxDist;

    //Small Player Stats
    public float smallJumpForce;
    public float smallMoveSpeed;
    public float smallMaxVelocity;
    public float smallMaxDist;

    public PlayerController playerController;

    private void Start()
    {
        targetScale = transform.localScale;
        cam = Camera.main;
    }

    private void Update()
    {
        if (transform.localScale != targetScale)
        {
            lerpFloat = Mathf.Clamp01(lerpFloat + Time.deltaTime / scaleTime + 0.001f);

            transform.localScale = Vector3.Lerp(oldScale, targetScale, lerpFloat);
            cam.orthographicSize = 1 + transform.localScale.x * 4;
        }

        if (switchAction.WasPressedThisFrame())
        {
            bool canScale = true;
            if (!isBig)
            {
                canScale = !Physics2D.Raycast(transform.position, Vector2.up, scaleUpDist, groundMask);
            }
            if (canScale)
            {
                isBig = !isBig;
                targetScale = isBig ? bigScale : smallScale;
                lerpFloat = 0f;
                oldScale = transform.localScale;
            }
        }

        //Set variables
        if (isBig)
        {
            Physics2D.gravity = new Vector2(0, -20);
            playerController.jumpForce = bigJumpForce;
            playerController.moveSpeed = bigMoveSpeed;
            playerController.maxVelocity = bigMaxVelocity;
            playerController.groundDist = bigMaxDist;
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
            playerController.jumpForce = smallJumpForce;
            playerController.moveSpeed = smallMoveSpeed;
            playerController.maxVelocity = smallMaxVelocity;
            playerController.groundDist = smallMaxDist;
        }
    }

    private void OnEnable()
    {
        switchAction.Enable();
    }

    private void OnDisable()
    {
        switchAction.Disable();
    }
}
