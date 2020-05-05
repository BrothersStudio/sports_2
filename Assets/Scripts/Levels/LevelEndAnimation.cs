using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndAnimation : MonoBehaviour
{
    private int current_level;
    private TextSystem text_system;

    private void Awake()
    {
        text_system = FindObjectOfType<TextSystem>();
    }

    public void SetCurrentLevel(int current_level)
    {
        this.current_level = current_level;
    }

    public void Play()
    {
        FindObjectOfType<BallSpawner>().DoneLevel();
        FindObjectOfType<BallCounter>().Countdown();
        StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        // Move camera to goal
        FindObjectOfType<FollowPlayer>().EndLevelAnimation();

        yield return new WaitForSeconds(3);

        text_system.Activate();
    }
}
