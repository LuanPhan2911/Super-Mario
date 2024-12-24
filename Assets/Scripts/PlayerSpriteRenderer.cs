using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private PlayerMovement movement;
    public SpriteRenderer spriteRenderer { get; private set; }

    public Sprite idle;
    public Sprite slide;

    public Sprite jump;

    public AnimateRenderer run;
    private void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        run.enabled = true;
    }
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

    private void LateUpdate()
    {
        run.enabled = movement.isRunning;

        if (movement.isJumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (movement.isSliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!movement.isRunning)
        {
            spriteRenderer.sprite = idle;
        }

    }
}
