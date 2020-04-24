using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip puzzle_music;

    public void SetMusic(int level)
    {
        if (level == 1)
        {
            GetComponent<AudioSource>().clip = puzzle_music;
            GetComponent<AudioSource>().Play();
        }
    }
}
