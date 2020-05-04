using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateDrag : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    private List<float> last_seen_drags = new List<float>();
    private int drag_history_length = 10;

    private bool rpg_mode = false;
    private float rpg_mode_force = 1;

    private void Awake()
    {
        rigidbody = transform.parent.GetComponent<Rigidbody2D>();

        for (int i = 0; i < drag_history_length; i++)
        {
            last_seen_drags.Add(rigidbody.drag);
        }
    }

    public void SetRPGMode()
    {
        rpg_mode = true;
        rpg_mode_force = 5;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ice")
        {
            Ice current_ice = collision.GetComponent<Ice>();
            UpdateDrag(current_ice.GetDrag());

            if (current_ice.brushed)
            {
                Vector2 force = (current_ice.transform.position - transform.position).normalized * current_ice.brush_force * rpg_mode_force;

                rigidbody.AddForce(force);
                if (force.x > 0)
                {
                    rigidbody.AddTorque(-1.5f);
                }
                else
                {
                    rigidbody.AddTorque(1.5f);
                }
            }
        }
    }

    private void UpdateDrag(float ice_drag)
    {
        if (!rpg_mode)
        {
            last_seen_drags.RemoveAt(0);
            last_seen_drags.Add(ice_drag);

            float sum = 0;
            foreach (float drag in last_seen_drags)
            {
                sum += drag;
            }
            rigidbody.drag = sum / (float)drag_history_length;
        }
    }
}
