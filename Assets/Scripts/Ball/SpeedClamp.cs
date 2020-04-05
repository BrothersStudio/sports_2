using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedClamp : MonoBehaviour
{
    public int max_speed;

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, max_speed);
    }
}
