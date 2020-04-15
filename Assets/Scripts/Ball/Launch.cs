using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public bool launched = false;
    public bool launching = false;
    public float launch_velocity;

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
            Vector3 new_pos = new Vector3(x_pos, transform.position.y + Time.deltaTime * launch_velocity, transform.position.z);
            GetComponent<Rigidbody2D>().MovePosition(new_pos);
        }
    }

    private void StartLaunching()
    {
        Cursor.visible = false;
        launching = true;
        launch_time = Time.timeSinceLevelLoad;

        main_camera.LaunchStart();
    }

    private void StopLaunching()
    {
        launched = true;

        Cursor.visible = true;
        launching = false;

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, launch_velocity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Launch Line")
        {
            StopLaunching();
        }
    }
}
