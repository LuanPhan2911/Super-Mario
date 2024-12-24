using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }
    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
        CircleCollider2D physicCollider = GetComponent<CircleCollider2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        triggerCollider.enabled = false;
        physicCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapseTime = 0;
        float duration = 0.5f;

        Vector3 startPosition = transform.localPosition;
        Vector3 endPosition = transform.localPosition + Vector3.up;
        while (elapseTime < duration)
        {

            transform.localPosition = Vector2.Lerp(startPosition, endPosition, elapseTime / duration);
            elapseTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPosition;

        rigidbody.isKinematic = false;
        triggerCollider.enabled = true;
        physicCollider.enabled = true;


    }
}
