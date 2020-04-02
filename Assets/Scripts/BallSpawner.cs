using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject point_ball_prefab;
    public GameObject player;

    public List<GameObject> spawn_zones;

    public void SpawnNew()
    {
        int tries = 0;
        while (tries < 20)
        {
            int roll = Random.Range(0, spawn_zones.Count);
            if (!spawn_zones[roll].GetComponent<SpawnBox>().on_me)
            {
                Instantiate(point_ball_prefab, spawn_zones[roll].transform.position, Quaternion.identity, transform);
                break;
            }
            tries++;
        }
    }
}
