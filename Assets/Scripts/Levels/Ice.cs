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

    private void Start()
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
        brushed_mask_color.a = 1/9f;
        brushed_mask.color = brushed_mask_color;
    }

    private void Unbrush()
    {
        brushed = false;
        current_friction = starting_friction;

        Color brushed_mask_color = brushed_mask.color;
        brushed_mask_color.a = 0;
        brushed_mask.color = brushed_mask_color;
    }

    public float GetDrag()
    {
        return current_friction;
    }
}