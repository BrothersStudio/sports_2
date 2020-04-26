using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    private float angle = 0;
    public bool start_right;

    private float speed = (2 * Mathf.PI) / 10;
    private float radius = 10;

    private float starting_x;
    private float starting_y;

    private void Start()
    {
        starting_x = transform.position.x;
        starting_y = transform.position.y;
    }

    void Update()
    {
        angle += speed * Time.deltaTime; 
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector3(starting_x + x, starting_y + y, -4);
    }
}
