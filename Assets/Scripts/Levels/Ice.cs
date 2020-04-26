using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float size;

    public float starting_friction;
    public float brushed_friction;
    private float current_friction;
    
    public bool brushed = false;
    private Brush brush;
    public float brush_force;
    public SpriteRenderer brushed_mask;
    private float unbrush_time = 10;

    public List<Ice> neighbors = new List<Ice>();
    private List<Vector3> neighbor_vectors = new List<Vector3>();

    private void Start()
    {
        transform.localScale = new Vector3(size, size, 1);

        current_friction = starting_friction;

        neighbor_vectors.Add(new Vector2(0, size / 100f));                            // north
        neighbor_vectors.Add(new Vector2(size / 100f * 1.4f, size / 100f * 1.4f));    // northeast
        neighbor_vectors.Add(new Vector2(size / 100f, 0));                            // east
        neighbor_vectors.Add(new Vector2(size / 100f * 1.4f, size / 100f * -1.4f));   // southeast
        neighbor_vectors.Add(new Vector2(0, -size / 100f));                           // south
        neighbor_vectors.Add(new Vector2(size / 100f * -1.4f, size / 100f * -1.4f));  // southwest
        neighbor_vectors.Add(new Vector2(size / 100f * -1.4f, 0));                    // west
        neighbor_vectors.Add(new Vector2(size / 100f * -1.4f, size / 100f * 1.4f));   // northwest
    }

    public void CalculateNeighbors()
    {
        foreach (Vector3 vector in neighbor_vectors)
        {
            bool found_ice = false;
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position + vector);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Ice")
                {
                    neighbors.Add(collider.GetComponent<Ice>());
                    found_ice = true;
                    break;
                }
            }

            if (!found_ice)
            {
                neighbors.Add(null);
            }
        }
    }

    public void SetBrush(Brush brush)
    {
        this.brush = brush;
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && brush.brushing && !brushed)
        {
            Brush();
        }
    }

    public void Brush(bool init = false)
    {
        brushed = true;
        current_friction = brushed_friction;

        if (!init)
        {
            brush.SpawnBrushParticles();
            Invoke("Unbrush", unbrush_time);
        }

        Color brushed_mask_color = brushed_mask.color;
        brushed_mask_color.a = GetBrushedNeighbors() / 9f;
        brushed_mask.color = brushed_mask_color;
    }

    private void Unbrush()
    {
        brushed = false;
        current_friction = starting_friction;

        Color brushed_mask_color = brushed_mask.color;
        brushed_mask_color.a = GetBrushedNeighbors() / 9f;
        brushed_mask.color = brushed_mask_color;
    }

    private int GetBrushedNeighbors()
    {
        int count = 0;
        foreach (Ice ice in neighbors)
        {
            if (ice != null)
            {
                if (ice.brushed)
                {
                    count++;
                }
            }
        }

        if (brushed)
        {
            count++;
        }

        return count;
    }

    public float GetDrag()
    {
        return current_friction;
    }
}