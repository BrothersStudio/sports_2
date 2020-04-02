using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    void FixedUpdate()
    {
        Vector3 position = new Vector3(transform.position.x, player.transform.position.y + 1.5f, -10);
        transform.position = position;
    }
}
