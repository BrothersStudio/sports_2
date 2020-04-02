using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y <= -5.41f)
        {
            Vector3 new_position = new Vector3(transform.position.x, 5.4f, 0);
            transform.position = new_position;
        }
        else if (transform.position.y >= 5.41f)
        {
            Vector3 new_position = new Vector3(transform.position.x, -5.4f, 0);
            transform.position = new_position;
        }
    }
}
