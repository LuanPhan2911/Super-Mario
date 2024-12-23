using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{

    public Sprite sprite;

    private bool isShelled = false;
    private bool isPushed = false;

    public float shellSpeed = 12f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (!isShelled && other.gameObject.CompareTag("Player"))
        {
            if (other.transform.DotTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {

                player.Hit();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isShelled && other.gameObject.CompareTag("Player"))
        {

            if (!isPushed)
            {
                Vector2 direction = new(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                Player player = other.gameObject.GetComponent<Player>();
                player.Hit();
            }
        }

        if (!isShelled && other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void EnterShell()
    {
        isShelled = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimateRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = sprite;

    }

    private void PushShell(Vector2 direction)
    {
        isPushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;


        GetComponent<EntityMovement>().enabled = true;
        GetComponent<EntityMovement>().direction = direction.normalized;
        GetComponent<EntityMovement>().speed = shellSpeed;

        this.gameObject.layer = LayerMask.NameToLayer("Shell");


    }

    private void Hit()
    {
        GetComponent<AnimateRenderer>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;

        Destroy(gameObject, 3f);

    }
}
