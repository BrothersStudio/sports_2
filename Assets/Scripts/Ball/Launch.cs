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

    private float launch_time;
    private float click_release_sanctuary = 0.5f;

    private FollowPlayer main_camera;

    private void Awake()
    {
        main_camera = Camera.main.GetComponent<FollowPlayer>();
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
            if (!Input.GetMouseButton(0) && Time.timeSinceLevelLoad > launch_time + click_release_sanctuary)
            {
                StopLaunching();
                return;
            }

            float x_pos = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -4.5f, 4.5f);
            Vector3 new_pos = new Vector3(x_pos, transform.position.y + Time.deltaTime * current_velocity, transform.position.z);
            GetComponent<Rigidbody2D>().MovePosition(new_pos);
        }
    }

    private void StartLaunching()
    {
        Cursor.visible = false;
        launching = true;
        launch_time = Time.timeSinceLevelLoad;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        main_camera.LaunchStart();
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
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, current_velocity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Launch Line")
        {
            StopLaunching();
        }
    }
}
