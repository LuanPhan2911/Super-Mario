using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float speed = 8.0f;

    private Vector2 velocity;
    private float inputAxis;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        this.HorizontalMovement();
    }
    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;

        position += this.velocity * Time.fixedDeltaTime;

        this.rigidbody.MovePosition(position);
    }
    private void HorizontalMovement()
    {
        this.inputAxis = Input.GetAxis("Horizontal");
        this.velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * this.speed, this.speed * Time.deltaTime);
    }

}
