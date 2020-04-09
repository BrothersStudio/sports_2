using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float size;

    public float starting_friction;
    public float brushed_friction;
    private float current_friction;

    public Color brushed_color;
    public Color unbrushed_color;

    public bool brushed = false;
    private Brush brush;
    public float brush_force;

    public List<Ice> neighbors = new List<Ice>();
    private List<Vector3> neighbor_vectors = new List<Vector3>();

    private SlopeDirection slope;
    public float slope_force;

    private void Awake()
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
        GetComponent<SpriteRenderer>().color = Color.Lerp(unbrushed_color, brushed_color, GetBrushedNeighbors() / 9f);

        if (!init)
        {
            brush.SpawnBrushParticles();
        }

        brushed = true;
        current_friction = brushed_friction;
    }

    private int GetBrushedNeighbors()
    {
        int count = 1;
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
        return count;
    }

    public float GetFriction()
    {
        return current_friction;
    }

    public Vector2 GetForce()
    {
        Vector3 neighbor_force = Vector2.zero;

        for (int i = 0; i < neighbors.Count; i++)
        {
            if (neighbors[i] != null)
            {
                if (neighbors[i].GetFriction() < GetFriction())
                {
                    neighbor_force += neighbor_vectors[i];
                }
            }
        }

        return neighbor_force * brush_force + GetSlopeDirection() * slope_force; 
    }

    public void SetSlopeDirection(SlopeDirection slope)
    {
        this.slope = slope;

        if (slope != SlopeDirection.None)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private Vector3 GetSlopeDirection()
    {
        switch (slope)
        {
            case SlopeDirection.North:
                return Vector3.up;
            case SlopeDirection.East:
                return Vector3.right;
            case SlopeDirection.South:
                return Vector3.down;
            case SlopeDirection.West:
                return Vector3.left;
            default:  // No slope
                return Vector3.zero;
        }
    }
}

public enum SlopeDirection
{
    None,
    North,
    East,
    South,
    West
}
