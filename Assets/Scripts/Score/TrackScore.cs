using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScore : MonoBehaviour
{
    float total_score;

    private Vector3 goal_pos;
    private List<Transform> active_balls = new List<Transform>();

    public float score_multiplier;

    private void Start()
    {
        goal_pos = GameObject.Find("Goal").transform.position;
        goal_pos.z = 0;
    }

    public void RegisterNewBall(Transform new_ball)
    {
        active_balls.Add(new_ball);
    }

    private void Update()
    {
        CalculateScore();
    }

    public string CalculateScore()
    {
        float total_score = 0;
        foreach (Transform ball in active_balls)
        {
            Vector3 ball_pos = ball.position;
            ball_pos.z = 0;
            float distance = Vector3.Distance(goal_pos, ball_pos);
            total_score += Mathf.Clamp(score_multiplier - (distance / 39f) * score_multiplier, 0, score_multiplier);
        }

        string score_string = total_score.ToString("0.");
        GetComponent<Text>().text = score_string;

        return score_string;
    }
}
