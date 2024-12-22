using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{

    private Transform player;


    private void Awake()
    {
        this.player = GameObject.FindWithTag("Player").transform;

    }
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);




        this.transform.position = cameraPosition;
    }
}
