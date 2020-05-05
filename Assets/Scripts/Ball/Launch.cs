using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public bool launched = false;
    public bool launching = false;

    private float current_velocity;
    public float launch_velocity;
    private float smooth_launch_time = 3;

    private float current_bounds;
    private float narrow_start_bounds = 4.4f;
    private float wide_start_bounds = 9.9f;

    private FollowPlayer main_camera;
    private Brush brush;

    private void Awake()
    {
        main_camera = Camera.main.GetComponent<FollowPlayer>();
        brush = FindObjectOfType<Brush>();
    }

    private void Start()
    {
        if (FindObjectOfType<Level>().narrow_start)
        {
            current_bounds = narrow_start_bounds;
        }
        else
        {
            current_bounds = wide_start_bounds;
        }
    }

    private void OnMouseDown()
    {
        if (!launched)
        {
            StartLaunching();
        }
    }

    private void FixedUpdate()
    {
        if (launching)
        {
            float x_pos = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -current_bounds, current_bounds);
            Vector3 new_pos = new Vector3(x_pos, transform.position.y + Time.deltaTime * current_velocity, transform.position.z);
            GetComponent<Rigidbody2D>().MovePosition(new_pos);
        }
    }

    private void StartLaunching()
    {
        Cursor.visible = false;
        launching = true;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        main_camera.LaunchStart();
        FindObjectOfType<Tutorial>().Clicked();

        StartCoroutine(SmoothVelocity());
    }

    private IEnumerator SmoothVelocity()
    {
        float t = 0;
        while (current_velocity < launch_velocity)
        {
            current_velocity = Mathf.SmoothStep(launch_velocity / 2f, launch_velocity, t);
            t += Time.deltaTime / smooth_launch_time;
            yield return null;
        }
    }

    private void StopLaunching()
    {
        launched = true;
        launching = false;

        main_camera.LaunchEnd();
        FindObjectOfType<Tutorial>().Launched();
        brush.Brushing(true);

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, current_velocity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lines")
        {
            StopLaunching();
        }
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
