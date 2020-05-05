using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Vector2 lower_left_point;
    public Vector2 upper_right_point;

    public GameObject ice_square;

    private Brush brush;
    private IcePool ice_pool;
    private List<GameObject> all_ice;

    private void Awake()
    {
        brush = FindObjectOfType<Brush>();
        ice_pool = FindObjectOfType<IcePool>();
    }

    private void Start()
    {
        all_ice = new List<GameObject>();
        float x_distance = ice_square.GetComponent<Ice>().size / 100f;
        float y_distance = ice_square.GetComponent<Ice>().size / 100f;
        for (float i = lower_left_point.y; i < upper_right_point.y; i += y_distance)
        {
            for (float j = lower_left_point.x; j < upper_right_point.x; j += x_distance)
            {
                Vector3 position = new Vector3(j, i, 0);
                if (IsValidIcePosition(position))
                {
                    GameObject ice = ice_pool.GetIce();
                    ice.transform.position = position;
                    ice.transform.SetParent(transform);
                    ice.SetActive(true);
                    all_ice.Add(ice);
                }
            }
        }

        foreach (GameObject ice in all_ice)
        {
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

    public void CleanupIce()
    {
        foreach (GameObject ice in all_ice)
        {
            ice.SetActive(false);
        }
    }
}
