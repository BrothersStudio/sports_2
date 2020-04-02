using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject ledge_prefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 new_ledge_position = new Vector3(Random.value < 0.5 ? -4 : 4, transform.position.y + 10.5f, 0);
        Debug.Log(new_ledge_position.x);
        Instantiate(ledge_prefab, new_ledge_position, Quaternion.identity, transform.parent);

        transform.position += new Vector3(0, 7, 0);
    }
}
