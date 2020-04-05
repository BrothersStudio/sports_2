using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    public Rigidbody2D active_ball;

    public bool brushing = false;

    private void Update()
    {
        if (active_ball.velocity.magnitude > 0.1f)
        {
            brushing = true;
        }
        else
        {
            brushing = false;
        }
    }
}
