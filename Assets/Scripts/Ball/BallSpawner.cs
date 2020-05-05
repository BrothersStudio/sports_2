using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Vector2 spawn_location;

    private int current_level = 0;
    private bool done_level = false;
    private int ball_count = 0;
    private List<GameObject> active_balls = new List<GameObject>();
    public GameObject ball_prefab;

    public AudioClip clapping_clip;

    private FollowPlayer main_camera;
    private Brush brush;

    private void Awake()
    {
        main_camera = FindObjectOfType<FollowPlayer>();
        brush = FindObjectOfType<Brush>();
    }

    private void Start()
    {
        active_balls.Add(SpawnNewBall());
    }

    public void SetCurrentLevel(int current_level)
    {
        this.current_level = current_level;    
    }

    private GameObject SpawnNewBall()
    {
        ball_count++;
        done_level = false;

        Vector3 total_spawn_location = new Vector3(spawn_location.x, spawn_location.y, ball_prefab.transform.position.z);
        GameObject new_ball = Instantiate(ball_prefab, total_spawn_location, ball_prefab.transform.rotation);
        new_ball.GetComponent<Ball>().SetLevel(current_level);

        main_camera.RegisterNewBall(new_ball.transform);
        brush.RegisterNewBall(new_ball.transform);
        FindObjectOfType<BallCounter>().Countdown();

        return new_ball;
    }

    private void Update()
    {
        if (AllBallsDoneMoving())
        {
            brush.Brushing(false);

            if (active_balls.Count < 3)
            {
                Cursor.visible = true;
                PlayClapping();

                active_balls.Add(SpawnNewBall());
            }
            else if (!done_level)
            {
                done_level = true;
                Cursor.visible = true;
                PlayClapping();

                FindObjectOfType<BallCounter>().Countdown();
                FindObjectOfType<LevelEndAnimation>().Play();
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

    public void PlayClapping()
    {
        if (current_level == 1)
        {
            main_camera.GetComponent<AudioSource>().clip = clapping_clip;
            main_camera.GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            main_camera.GetComponent<AudioSource>().Play();
        }
    }

    public List<GameObject> GetBalls()
    {
        return active_balls;
    }

    public void CleanupBalls()
    {
        foreach (GameObject ball in active_balls)
        {
            Destroy(ball);
        }
        active_balls.Clear();
    }
}
