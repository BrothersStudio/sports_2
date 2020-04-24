using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDrag : MonoBehaviour
{
    Rigidbody2D rgb;
    List<float> last_seen_drags = new List<float>();
    int drag_history_length = 10;

    private void Awake()
    {
        rgb = transform.parent.GetComponent<Rigidbody2D>();

        for (int i = 0; i < drag_history_length; i++)
        {
            last_seen_drags.Add(rgb.drag);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ice")
        {
            Ice current_ice = collision.GetComponent<Ice>();
            UpdateDrag(current_ice.GetDrag());

            if (current_ice.brushed)
            {
                Vector2 force = (current_ice.transform.position - transform.position).normalized * current_ice.brush_force;

                rgb.AddForce(force);
                if (force.x > 0)
                {
                    rgb.AddTorque(-1.5f);
                }
                else
                {
                    rgb.AddTorque(1.5f);
                }
            }
        }
    }

    private void UpdateDrag(float ice_drag)
    {
        last_seen_drags.RemoveAt(0);
        last_seen_drags.Add(ice_drag);

        float sum = 0;
        foreach (float drag in last_seen_drags)
        {
            sum += drag;
        }
        rgb.drag = sum / (float)drag_history_length;
    }
}
