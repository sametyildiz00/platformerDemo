using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.position.x, -0.0f, 5.5f), Mathf.Clamp(player.position.y, -0.2f,0.2f), -10.0f);
    }
}
