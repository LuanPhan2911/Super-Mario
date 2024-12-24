using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private new Collider2D collider;

    public float speed = 8.0f;

    private Vector2 velocity;
    private float inputAxis;

    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public bool isGrounded { get; private set; }
    public bool isJumping { get; private set; }

    public bool isSliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    public bool isRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;


    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        isJumping = false;
    }
    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        isJumping = false;
    }
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.camera = Camera.main;
        this.collider = GetComponent<Collider2D>();
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        this.HorizontalMovement();
        this.isGrounded = rigidbody.Raycast(Vector2.down);

        if (isGrounded)
        {
            this.GroundedMovement();
        }
        this.ApplyGravity();


    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;

        position += this.velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        this.rigidbody.MovePosition(position);



    }
    private void GroundedMovement()
    {
        this.velocity.y = Mathf.Max(this.velocity.y, 0f);
        this.isJumping = velocity.y > 0;


        if (Input.GetButtonDown("Jump"))
        {
            this.velocity.y = jumpForce;
            this.isJumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool isFalling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = isFalling ? 2f : 1f;
        this.velocity.y += gravity * multiplier * Time.deltaTime;
        this.velocity.y = Mathf.Max(this.velocity.y, gravity / 2f);
    }

    private void HorizontalMovement()
    {
        this.inputAxis = Input.GetAxis("Horizontal");
        this.velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * this.speed, this.speed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0;
        }
        if (this.velocity.x > 0f)
        {
            this.transform.eulerAngles = Vector3.zero;
        }
        else if (this.velocity.x < 0f)
        {
            this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (this.transform.DotTest(other.transform, Vector2.down))
            {
                this.velocity.y = this.jumpForce / 2f;
                this.isJumping = true;
            }

        }
        else if (other.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            if (this.transform.DotTest(other.transform, Vector2.up))
            {
                this.velocity.y = 0f;
            }
        }
    }

}
