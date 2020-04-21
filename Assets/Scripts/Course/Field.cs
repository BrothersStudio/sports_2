using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector2 lower_left_point;
    public Vector2 upper_right_point;

    public GameObject ice_square;

    private Brush brush;

    private void Awake()
    {
        brush = FindObjectOfType<Brush>();
    }

    private void Start()
    {
        List<GameObject> all_ice = new List<GameObject>();
        float x_distance = ice_square.GetComponent<Ice>().size / 100f;
        float y_distance = ice_square.GetComponent<Ice>().size / 100f;
        for (float i = lower_left_point.y; i < upper_right_point.y; i += y_distance)
        {
            for (float j = lower_left_point.x; j < upper_right_point.x; j += x_distance)
            {
                Vector3 position = new Vector3(j, i, 0);
                if (IsValidIcePosition(position))
                {
                    GameObject ice = Instantiate(ice_square, position, Quaternion.identity, transform);
                    ice.name = "Ice " + j.ToString() + " " + i.ToString();
                    ice.GetComponent<Ice>().SetBrush(brush);
                    all_ice.Add(ice);
                }
            }
        }

        foreach (GameObject ice in all_ice)
        {
            ice.GetComponent<Ice>().CalculateNeighbors();
            if (Random.value < 0.05f)
            {
                ice.GetComponent<Ice>().Brush(true);
            }
        }
    }

    private bool IsValidIcePosition(Vector3 position)
    {
        if (Physics2D.OverlapPoint(position, 1 << 8) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
