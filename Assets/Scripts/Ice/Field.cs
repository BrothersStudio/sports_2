﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector2 lower_left_point;
    public Vector2 field_size;
    public GameObject ice_square;

    public Brush brush;

    private void Awake()
    {
        List<GameObject> all_ice = new List<GameObject>();
        float x_distance = ice_square.GetComponent<Ice>().size / 100f;
        float y_distance = ice_square.GetComponent<Ice>().size / 100f;
        for (float i = lower_left_point.y; i < lower_left_point.y + field_size.y * y_distance; i += y_distance)
        {
            for (float j = lower_left_point.x; j < lower_left_point.x + field_size.x * x_distance; j += x_distance)
            {
                Vector3 position = new Vector3(j, i, 0);
                GameObject ice = Instantiate(ice_square, position, Quaternion.identity, transform);
                ice.name = "Ice " + j.ToString() + " " + i.ToString();
                ice.GetComponent<Ice>().SetBrush(brush);

                if (i > 4 && i < 7)
                {
                    ice.GetComponent<Ice>().SetSlopeDirection(SlopeDirection.North);
                }

                all_ice.Add(ice);
            }
        }

        foreach (GameObject ice in all_ice)
        {
            ice.GetComponent<Ice>().CalculateNeighbors();
        }
    }
}
