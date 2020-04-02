using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySphere : MonoBehaviour
{
    private void Start()
    {
        Vector2 velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        GetComponent<Rigidbody2D>().velocity = velocity.normalized;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<WreckingBall>().dangerous)
            {
                FindObjectOfType<BallSpawner>().SpawnNew();
                Destroy(gameObject);
            }
        }
    }
}
