using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Transform main_camera;
    private Vector3 last_pos;
    private float parallax_percent = 20;

    private void Awake()
    {
        main_camera = Camera.main.transform;
        last_pos = main_camera.position;
    }

    private void LateUpdate()
    {
        Vector3 diff = main_camera.position - last_pos;
        diff *= parallax_percent / 100f;
        transform.position += diff;
        last_pos = main_camera.position;
    }
}
