using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCounter : MonoBehaviour
{
    private int current_ball = 0;
    public List<Image> counters;
    public Sprite stone_sprite;
    public Sprite grenade_sprite;
    public Sprite rpg_sprite;

    public void SetCurrentLevel(int level)
    {
        current_ball = 0;

        foreach (Image counter in counters)
        {
            counter.enabled = true;
            if (level >= 6)
            {
                counter.sprite = rpg_sprite;
            }
            else if (level >= 2)
            {
                counter.sprite = grenade_sprite;
            }
            else
            {
                counter.sprite = stone_sprite;
            }
        }
    }

    public void Countdown()
    {
        current_ball++;
        counters[counters.Count - current_ball].enabled = false;
    }
}
