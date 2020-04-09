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
            rgb.drag = current_ice.GetFriction();
            rgb.AddForce(current_ice.GetForce());
        }
    }
}
