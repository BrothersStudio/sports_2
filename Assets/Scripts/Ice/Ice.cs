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

    private Brush brush;

    private void Awake()
    {
        transform.localScale = new Vector3(size, size, 1);

        current_friction = starting_friction;
    }

    public void SetBrush(Brush brush)
    {
        this.brush = brush;
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0) && brush.brushing)
        {
            GetComponent<SpriteRenderer>().color = brushed_color;
            current_friction = brushed_friction;
        }
    }

    public float GetFriction()
    {
        return current_friction;
    }
}
