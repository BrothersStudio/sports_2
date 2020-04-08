using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float size;

    public float starting_friction;
    public float brushed_friction;

    public bool brushed = false;
    private float current_friction;

    public Color brushed_color;
    public Color unbrushed_color;

    private Brush brush;

    public Vector2 position = new Vector2();

    public List<Ice> neighbors;
    private List<Vector3> neighbor_vectors = new List<Vector3>();

    private void Awake()
    {
        transform.localScale = new Vector3(size, size, 1);

        current_friction = starting_friction;

        neighbor_vectors.Add(new Vector2(0, size / 100f));  // north
        neighbor_vectors.Add(new Vector2(size / 100f * 1.4f, size / 100f * 1.4f));  // northeast
        neighbor_vectors.Add(new Vector2(size / 100f, 0));  // east
        neighbor_vectors.Add(new Vector2(size / 100f * 1.4f, size / 100f * -1.4f)); // southeast
        neighbor_vectors.Add(new Vector2(0, -size / 100f));  // south
        neighbor_vectors.Add(new Vector2(size / 100f * -1.4f, size / 100f * -1.4f));  // southwest
        neighbor_vectors.Add(new Vector2(size / 100f * -1.4f, 0));  // west
        neighbor_vectors.Add(new Vector2(size / 100f * -1.4f, size / 100f * 1.4f)); // northwest
    }

    public void CalculateNeighbors()
    {
        foreach (Vector3 vector in neighbor_vectors)
        {
            foreach (Collider2D collider in Physics2D.OverlapPointAll(transform.position + vector))
            {
                if (collider.tag == "Ice")
                {
                    neighbors.Add(collider.GetComponent<Ice>());
                }
            }
        }
    }

    public void SetBrushAndPosition(Brush brush, int row, int col)
    {
        this.brush = brush;
        position.x = row;
        position.y = col;
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && brush.brushing && !brushed)
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(unbrushed_color, brushed_color, GetBrushedNeighbors() / 8f);

            brushed = true;
            current_friction = brushed_friction;
        }
    }

    private int GetBrushedNeighbors()
    {
        int count = 0;
        foreach (Ice ice in neighbors)
        {
            if (ice.brushed)
            {
                count++;
            }
        }
        return count;
    }

    public float GetFriction()
    {
        return current_friction;
    }
}
