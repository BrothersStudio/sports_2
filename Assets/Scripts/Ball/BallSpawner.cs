using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Vector2 spawn_location;

    private int ball_count = 0;
    private List<GameObject> active_balls = new List<GameObject>();
    public GameObject ball_prefab;

    public FollowPlayer main_camera;
    public TrackScore score_tracker;
    public Brush brush;

    private void Awake()
    {
        active_balls.Add(SpawnNewBall());
    }

    private GameObject SpawnNewBall()
    {
        ball_count++;

        Vector3 total_spawn_location = new Vector3(spawn_location.x, spawn_location.y, ball_prefab.transform.position.z);
        GameObject new_ball = Instantiate(ball_prefab, total_spawn_location, Quaternion.identity);

        main_camera.RegisterNewBall(new_ball.transform);
        score_tracker.RegisterNewBall(new_ball.transform);
        brush.RegisterNewBall(new_ball.transform);

        return new_ball;
    }

    private void Update()
    {
        if (AllBallsDoneMoving())
        {
            active_balls.Add(SpawnNewBall());
        }
    }

    private bool AllBallsDoneMoving()
    {
        if (ball_count != 3)
        {
            // Are any balls still moving
            foreach (GameObject ball in active_balls)
            {
                if (!(ball.GetComponent<Launch>().launched &&
                    ball.GetComponent<Rigidbody2D>().velocity.magnitude < 0.1f))
                {
                    return false;
                }
            }

            // Set all velocities to zero
            foreach (GameObject ball in active_balls)
            {
                ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            return true;
        }

        return false;
    }
}
