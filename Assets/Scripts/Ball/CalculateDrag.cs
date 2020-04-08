using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDrag : MonoBehaviour
{
    Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ice current_ice = collision.GetComponent<Ice>();
        rigidbody.drag = current_ice.GetFriction();
        rigidbody.AddForce(current_ice.GetForce());
    }
}
