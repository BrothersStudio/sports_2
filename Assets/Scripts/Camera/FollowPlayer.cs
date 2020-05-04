using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private float current_speed;
    private float slow_speed = 0.05f;
    private float fast_speed = 0.8f;
    private float x_speed = 0.05f;
    private float current_pause_time;
    private float quick_pause_time = 1f;
    private float long_pause_time = 3f;
    private float move_time = 2f;

    private bool launched = false;
    private bool camera_move = false;
    private Transform focus;
    private List<Transform> all_focuses = new List<Transform>();
    private float last_focus_change = 0;
    private float focus_change_cooldown = 1;

    private float trauma = 0;
    private float max_angle = 0.01f;
    private float max_offset = 0.5f;

    private Vector3 default_position;
    private Quaternion default_rotation;

    public LevelNameDisplay level_name_display;
    private List<int> x_move_allowed_levels = new List<int>();
    private bool can_x_move = false;

    private void Awake()
    {
        current_speed = fast_speed;

        default_position = transform.position;
        default_rotation = transform.rotation;
        
        x_move_allowed_levels.Add(2);
        x_move_allowed_levels.Add(3);
    }

    public void SetCurrentLevel(int level)
    {
        // Clear old ball references
        all_focuses.Clear();

        // Can move in x direction?
        if (x_move_allowed_levels.Contains(level))
        {
            can_x_move = true;
        }
        else
        {
            can_x_move = false;
        }

        // How long to pause before moving to ball
        if (level >= 6)
        {
            current_pause_time = long_pause_time;
        }
        else
        {
            current_pause_time = quick_pause_time;
        }
    }

    public void SetFocus(Transform focus)
    {
        this.focus = focus;
        current_speed = slow_speed;
        last_focus_change = Time.timeSinceLevelLoad;
    }

    public void StartNewLevelOverview()
    {
        Vector3 position = FindObjectOfType<Goal>().transform.position;
        position.z = transform.position.z;
        transform.position = position;

        camera_move = false;
        StartCoroutine(LevelOverviewAnimation());
    }

    public void StartAnimationFromIntro()
    {
        StartCoroutine(LevelOverviewAnimation());
    }

    private IEnumerator LevelOverviewAnimation()
    {
        yield return new WaitForSeconds(current_pause_time);

        float t = 0;
        while (t < 1)
        {
            if (t > 0.10f)
            {
                level_name_display.TurnOff();
            }

            t += Time.deltaTime / move_time;
            Vector3 pos = Vector2.Lerp(transform.position, focus.transform.position, SmoothStep(t));
            pos.z = transform.position.z;
            transform.position = pos;
            yield return null;
        }
    }
    
    public void PauseCamera()
    {
        camera_move = false;
        Invoke("UnpauseCamera", 1.6f);
    }

    private void UnpauseCamera()
    {
        camera_move = true;
    }

    public void EndLevelAnimation()
    {
        all_focuses.Clear();
        SetFocus(FindObjectOfType<Goal>().transform);
    }

    private float SmoothStep(float t)
    {
        return t * t * (3 - 2 * t);
    }

    public void RegisterNewBall(Transform new_ball)
    {
        current_speed = slow_speed;

        SetFocus(new_ball);
        all_focuses.Add(new_ball);

        launched = false;
    }

    public void LaunchStart()
    {
        current_speed = fast_speed;
        level_name_display.TurnOff();

        camera_move = true;
        StopAllCoroutines();
    }

    public void LaunchEnd()
    {
        launched = true;
    }

    public void Shake(float amount)
    {
        trauma += amount;
    }

    private void FixedUpdate()
    {
        trauma = Mathf.Clamp01(trauma - 0.01f);
        if (trauma == 0)
        {
            transform.rotation = default_rotation;
        }
    }

    private void FindFocus()
    {
        if (launched && Time.timeSinceLevelLoad > last_focus_change + focus_change_cooldown)
        {
            foreach (Transform focus_check in all_focuses)
            {
                if (focus_check.GetComponent<Rigidbody2D>().velocity.magnitude > 
                    focus.GetComponent<Rigidbody2D>().velocity.magnitude)
                {
                    SetFocus(focus_check);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (trauma > 0)
        {
            float angle = max_angle * trauma * trauma * Random.Range(-1f, 1f);
            transform.Rotate(new Vector3(0, 0, angle));

            float offset_x = max_offset * trauma * trauma * Random.Range(-1f, 1f);
            float offset_y = max_offset * trauma * trauma * Random.Range(-1f, 1f);
            transform.Translate(new Vector2(offset_x, offset_y));
        }

        if (camera_move)
        {
            FindFocus();

            float new_y = transform.position.y * (1 - current_speed) + focus.position.y * current_speed;

            float new_x;
            if (!can_x_move || !launched)
            {
                new_x = default_position.x;
            }
            else 
            {
                new_x = transform.position.x * (1 - x_speed) + focus.position.x * x_speed;
            }


            // Speed camera back up if it's caught up with focus
            Vector3 new_location = new Vector3(new_x, new_y, transform.position.z);
            if (Vector3.Distance(new_location, transform.position) < 0.1f)
            {
                StartCoroutine(SlowlySpeedUpCamera());
            }

            transform.position = new_location;
        }
    }

    private IEnumerator SlowlySpeedUpCamera()
    {
        while (current_speed < fast_speed)
        {
            current_speed += Time.deltaTime / 5;
            yield return null;
        }
    }
}
