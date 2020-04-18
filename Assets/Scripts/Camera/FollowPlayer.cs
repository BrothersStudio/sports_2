using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float current_speed;
    private float slow_speed = 0.05f;
    private float fast_speed = 0.9f;
    private Transform player;

    private float trauma = 0;
    private float max_angle = 0.01f;
    private float max_offset = 0.5f;

    private Vector3 default_position;
    private Quaternion default_rotation;

    public LevelNameDisplay level_name_display;

    void Awake()
    {
        current_speed = fast_speed;

        default_position = transform.position;
        default_rotation = transform.rotation;
    }

    public void MoveToGoal(Vector2 goal_location)
    {
        Vector3 position = goal_location;
        position.z = -10;
        transform.position = position;
    }

    public void RegisterNewBall(Transform new_ball)
    {
        current_speed = slow_speed;

        player = new_ball;
    }

    public void LaunchStart()
    {
        current_speed = fast_speed;
        level_name_display.TurnOff();
    }

    public void Shake(float amount)
    {
        trauma += amount;
    }

    void Update()
    {
        trauma = Mathf.Clamp01(trauma - 0.01f);
        if (trauma == 0)
        {
            transform.rotation = default_rotation;
        }
    }

    void LateUpdate()
    {
        if (trauma > 0)
        {
            float angle = max_angle * trauma * trauma * Random.Range(-1f, 1f);
            transform.Rotate(new Vector3(0, 0, angle));

            float offset_x = max_offset * trauma * trauma * Random.Range(-1f, 1f);
            float offset_y = max_offset * trauma * trauma * Random.Range(-1f, 1f);
            transform.Translate(new Vector2(offset_x, offset_y));
        }
        else
        {
            float new_y = transform.position.y * (1 - current_speed) + player.position.y * current_speed;

            float new_x = default_position.x;
            //new_x = transform.position.x * (1 - follow_speed) + player.position.x * follow_speed;

            transform.position = new Vector3(new_x, new_y, -10);

            if (Vector2.Distance(player.transform.position, transform.position) < 0.1f)
            {
                LaunchStart();
            }
        }
    }
}
