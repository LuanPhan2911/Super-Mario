using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down;
    public Vector3 exitDirection = Vector3.zero;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    private IEnumerator Enter(Transform player)
    {

        player.GetComponent<PlayerMovement>().enabled = false;
        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return StartCoroutine(Move(player, enteredPosition, enteredScale));
        yield return new WaitForSeconds(1f);

        bool isUnderground = connection.position.y < 0;

        Camera.main.GetComponent<SideScrolling>().SetUnderground(isUnderground);
        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);

        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerMovement>().enabled = true;


    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapse = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapse < duration)
        {
            elapse += Time.deltaTime;
            player.position = Vector3.Lerp(startPosition, endPosition, elapse / duration);
            player.localScale = Vector3.Lerp(startScale, endScale, elapse / duration);
            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }

}
