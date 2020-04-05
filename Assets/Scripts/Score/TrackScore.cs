using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackScore : MonoBehaviour
{
    private Transform goal;
    private Transform active_ball;
    public float score_multiplier;

    private void Awake()
    {
        goal = GameObject.Find("Goal").transform;
        active_ball = GameObject.Find("Ball").transform;
    }

    private void Update()
    {
        CalculateScore();
    }

    void CalculateScore()
    {
        Vector3 ball_pos = active_ball.position;
        ball_pos.z = 0;

        // TODO: dont get the goal pos every frame, okay for now
        Vector3 goal_pos = goal.position;
        goal_pos.z = 0;

        float distance = Vector3.Distance(goal_pos, ball_pos);
        GetComponent<Text>().text = (score_multiplier - (distance / 19.5f) * score_multiplier).ToString("0.");
    }
}
