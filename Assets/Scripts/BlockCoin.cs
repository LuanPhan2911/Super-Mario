using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{

    void Start()
    {
        GameManager.instance.AddCoin();
        StartCoroutine(Animated());
    }

    private IEnumerator Animated()
    {


        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatedPosition);
        Destroy(gameObject);


    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float duration = 0.5f;
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
