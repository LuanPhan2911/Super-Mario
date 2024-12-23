using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite emptyBlock;

    public GameObject item;


    public int maxHits = -1;
    private bool isAnimating = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isAnimating && maxHits != 0 && other.gameObject.CompareTag("Player"))
        {
            if (other.transform.DotTest(this.transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        this.maxHits--;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        if (this.maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;

        }
        if (this.item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        StartCoroutine(Animated());


    }

    private IEnumerator Animated()
    {
        isAnimating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        isAnimating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float duration = 0.125f;
        float elapse = 0;
        while (elapse < duration)
        {
            transform.localPosition = Vector3.Lerp(from, to, elapse / duration);
            elapse += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = to;
    }
}
