using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer bigSpriteRenderer;
    public PlayerSpriteRenderer smallSpriteRenderer;
    private PlayerSpriteRenderer activeSpriteRenderer;

    private DeathAnimation deathAnimation;

    private CapsuleCollider2D capsuleCollider;

    public bool isBig => bigSpriteRenderer.enabled;
    public bool isSmall => smallSpriteRenderer.enabled;
    public bool isDead => deathAnimation.enabled;

    public bool isStarPowered = false;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeSpriteRenderer = smallSpriteRenderer;
    }

    public void Hit()
    {

        if (!isDead && !isStarPowered)
        {
            if (isBig)
            {
                Shrink();
            }
            else
            {
                Dead();
            }
        }
    }


    public void Grow()
    {
        bigSpriteRenderer.enabled = true;
        smallSpriteRenderer.enabled = false;
        activeSpriteRenderer = bigSpriteRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    public void Shrink()
    {
        bigSpriteRenderer.enabled = false;
        smallSpriteRenderer.enabled = true;

        activeSpriteRenderer = smallSpriteRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }


    private IEnumerator ScaleAnimation()
    {
        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                smallSpriteRenderer.enabled = !smallSpriteRenderer.enabled;
                bigSpriteRenderer.enabled = !smallSpriteRenderer.enabled;
            }
            yield return null;
        }

        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = false;

        activeSpriteRenderer.enabled = true;
    }

    public void StarPower(float duration = 10f)
    {

        StartCoroutine(StartPowerAnimation(duration));
    }

    private IEnumerator StartPowerAnimation(float duration)
    {
        isStarPowered = true;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (Time.frameCount % 4 == 0)
            {
                activeSpriteRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }
            yield return null;
        }
        activeSpriteRenderer.spriteRenderer.color = Color.white;
        isStarPowered = false;
    }
    private void Dead()
    {
        smallSpriteRenderer.enabled = false;
        bigSpriteRenderer.enabled = false;
        deathAnimation.enabled = true;

        // reset level after 3 seconds
        GameManager.instance.ResetLevel(3f);
    }
}
