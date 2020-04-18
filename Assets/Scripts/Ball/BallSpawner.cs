using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Vector2 spawn_location;

    private int ball_count = 0;
    private List<GameObject> active_balls = new List<GameObject>();
    public GameObject ball_prefab;

    private FollowPlayer main_camera;
    private TrackScore score_tracker;
    private Brush brush;
    private Scorecard scorecard;

    private void Awake()
    {
        main_camera = FindObjectOfType<FollowPlayer>();
        score_tracker = FindObjectOfType<TrackScore>();
        brush = FindObjectOfType<Brush>();
        scorecard = GameObject.Find("Canvas").transform.Find("Scorecard").GetComponent<Scorecard>();
    }

    private void Start()
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
            Cursor.visible = true;
            if (active_balls.Count < 3)
            {
                active_balls.Add(SpawnNewBall());
            }
            else
            {
                scorecard.gameObject.SetActive(true);
            }
        }
    }

    private bool AllBallsDoneMoving()
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

        // Set all lingering velocities to zero
        foreach (GameObject ball in active_balls)
        {
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        return true;
    }
}
