using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite sprite;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.transform.DotTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {

                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shell"))
        {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AnimateRenderer>().enabled = false;

        GetComponent<SpriteRenderer>().sprite = sprite;
        Destroy(gameObject, 2f);
    }

    private void Hit()
    {
        GetComponent<AnimateRenderer>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;

        Destroy(gameObject, 3f);

    }
}
