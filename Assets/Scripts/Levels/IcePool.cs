using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePool : MonoBehaviour
{
    public int ice_to_pool;
    public GameObject ice_prefab;
    public List<GameObject> pooled_ice = new List<GameObject>();

    private Brush brush;

    // Start is called before the first frame update
    void Awake()
    {
        brush = FindObjectOfType<Brush>();

        for (int i = 0; i < ice_to_pool; i++)
        {
            GameObject ice = Instantiate(ice_prefab, transform);
            ice.GetComponent<Ice>().SetBrush(brush);
            ice.SetActive(false);
            pooled_ice.Add(ice);
        }
    }

    public GameObject GetIce()
    {
        foreach (GameObject ice in pooled_ice)
        {
            if (!ice.activeSelf)
            {
                return ice;
            }
        }

        // Used all the ice
        GameObject new_ice = Instantiate(ice_prefab, transform);
        new_ice.GetComponent<Ice>().SetBrush(brush);
        new_ice.SetActive(false);
        pooled_ice.Add(new_ice);
        return new_ice;
    }
}
