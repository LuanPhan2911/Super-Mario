using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform bottomFlag;

    public Transform castle;

    public float speed = 6f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, bottomFlag.position));
            StartCoroutine(LevelCompleteSequence(other.transform));
        }
    }


    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        yield return StartCoroutine(MoveTo(player, bottomFlag.position));
        yield return StartCoroutine(MoveTo(player, player.position + Vector3.right));
        yield return StartCoroutine(MoveTo(player, player.position + Vector3.right + Vector3.down));

        yield return StartCoroutine(MoveTo(player, castle.position));

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.instance.LoadLevel(1, 1);
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            yield return null;

        }
        subject.position = destination;
    }
}
