using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool lerpCrouch;
    public bool crouching;
    public float crouchTimer;
    public bool sprinting;
    public bool lerpCrawl;
    public bool crawling;
    public float crawlTimer;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
                controller.height = Mathf.Lerp((controller.height), 1, p);
            else
                controller.height = Mathf.Lerp((controller.height), 2, p);
            
            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
        if (lerpCrawl)
        {
            crawlTimer += Time.deltaTime;
            float c = crawlTimer / 1;
            c *= c;
            if (crawling)
                controller.height = Mathf.Lerp((controller.height), 0.1f, c);
            else
                controller.height = Mathf.Lerp((controller.height), 2, c);
            
            if (c > 1)
            {
                lerpCrawl = false;
                crawlTimer = 0f;
            }
        }
    }
    //recieve the imputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
        if (crouching && !sprinting && !crawling)
            speed = 2;
        else
            speed = 5;
    }
    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting && !crouching && !crawling)
            speed = 8;
        else
            speed = 5;
    }
    public void Crawl()
    {
        crawling = !crawling;
        crawlTimer = 0;
        lerpCrawl = true;
        if (crawling && !sprinting && !crouching)
            speed = 1;
        else
            speed = 5;
    }
}
