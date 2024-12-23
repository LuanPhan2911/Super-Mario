using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateRenderer : MonoBehaviour
{
    public Sprite[] sprites;

    public float frameRate = 1f / 6f;

    public int frameIndex;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), frameRate, frameRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Animate()
    {
        frameIndex++;
        if (frameIndex >= sprites.Length)
        {
            frameIndex = 0;
        }
        if (frameIndex >= 0 && frameIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frameIndex];
        }
    }
}
