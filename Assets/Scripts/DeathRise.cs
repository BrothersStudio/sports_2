using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRise : MonoBehaviour
{
    public float speed;

    void FixedUpdate()
    {
        transform.position += new Vector3(0, speed, 0);
    }
}
