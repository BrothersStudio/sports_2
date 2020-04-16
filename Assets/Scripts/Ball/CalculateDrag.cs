using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDrag : MonoBehaviour
{
    Rigidbody2D rgb;

    private void Awake()
    {
        rgb = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ice")
        {
            Ice current_ice = collision.GetComponent<Ice>();
            if (current_ice.brushed)
            {
                Vector2 force = (current_ice.transform.position - transform.position).normalized * current_ice.brush_force;
                rgb.AddForce(force);
            }
        }
    }
}
