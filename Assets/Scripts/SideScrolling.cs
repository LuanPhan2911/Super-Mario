using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{

    private Transform player;

    public float height = 6.5f;
    public float undergroundHeight = -9f;


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

    public void SetUnderground(bool isUnderground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = isUnderground ? undergroundHeight : height;
        this.transform.position = cameraPosition;
    }
}
