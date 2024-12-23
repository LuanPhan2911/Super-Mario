using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Vector2 direction = Vector2.left;
    private Vector2 velocity;
    public float speed;
    public float gravity = -9.81f;

    private new Rigidbody2D rigidbody;
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }

    private void OnBecameVisible()
    {
        this.enabled = true;
    }

    private void OnBecameInvisible()
    {
        this.enabled = false;
    }

    private void OnEnable()
    {
        this.rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        this.rigidbody.velocity = Vector2.zero;
        this.rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        this.velocity.x = this.direction.x * this.speed;
        this.velocity.y += this.gravity * Time.fixedDeltaTime;

        this.rigidbody.MovePosition(
            this.rigidbody.position + this.velocity * Time.fixedDeltaTime);

        if (rigidbody.Raycast(this.direction))
        {
            this.direction = -this.direction;
        }
        if (rigidbody.Raycast(Vector2.down))
        {
            this.velocity.y = Mathf.Max(0, this.velocity.y);
        }
    }
}
