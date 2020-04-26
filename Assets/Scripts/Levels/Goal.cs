using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool dead = false;

    public void DisableAndCheckDone()
    {
        dead = true;

        // Stop moving
        CircleMove move = GetComponent<CircleMove>();
        if (move != null)
        {
            move.enabled = false;
        }

        // Are we done the level?
        Goal[] goals = FindObjectsOfType<Goal>();
        foreach (Goal goal in goals)
        {
            if (!goal.dead)
            {
                // Not done yet
                return;
            }
        }

        // Done!
        FindObjectOfType<LevelEndAnimation>().Play();
    }
}
