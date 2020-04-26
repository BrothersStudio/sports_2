using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScore : MonoBehaviour
{
    float total_score;

    private float initial_ball_dist;
    private Vector3 goal_pos;
    private List<Transform> active_balls = new List<Transform>();

    public float score_multiplier;

    public void RecordGoalPosition()
    {
        goal_pos = FindObjectOfType<Goal>().transform.position;
        goal_pos.z = 0;

        active_balls.Clear();
    }

    public void RegisterNewBall(Transform new_ball)
    {
        active_balls.Add(new_ball);

        Vector3 ball_pos = new_ball.position;
        ball_pos.z = 0;
        initial_ball_dist = Vector3.Distance(ball_pos, goal_pos);
    }

    private void Update()
    {
        CalculateScore();
    }

    private void CalculateScore()
    {
        float total_score = 0;
        foreach (Transform ball in active_balls)
        {
            Vector3 ball_pos = ball.position;
            ball_pos.z = 0;
            float distance = Vector3.Distance(goal_pos, ball_pos);
            total_score += Mathf.Clamp(score_multiplier - (distance / initial_ball_dist) * score_multiplier, 0, score_multiplier);
        }

        string score_string = total_score.ToString("0.");
        GetComponent<Text>().text = score_string;
    }
}
