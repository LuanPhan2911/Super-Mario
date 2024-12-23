using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerSpriteRenderer bigSpriteRenderer;
    public PlayerSpriteRenderer smallSpriteRenderer;

    private DeathAnimation deathAnimation;

    public bool isBig => bigSpriteRenderer.enabled;
    public bool isSmall => smallSpriteRenderer.enabled;
    public bool isDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit()
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

    private void Shrink()
    {
        // bigSpriteRenderer.enabled = false;
        // smallSpriteRenderer.enabled = true;
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
