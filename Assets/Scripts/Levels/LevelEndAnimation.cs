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
        FindObjectOfType<FollowPlayer>().EndLevelAnimation();

        //text_system.Activate();
    }
}
