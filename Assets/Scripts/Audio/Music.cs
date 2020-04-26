using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip puzzle_music;
    public AudioClip dialogue_music;
    public AudioClip fight_music;

    public void SetLevelMusic(int level)
    {
        if (level == 1)
        {
            GetComponent<AudioSource>().clip = puzzle_music;
            GetComponent<AudioSource>().Play();
        }
        else 
        {
            GetComponent<AudioSource>().clip = fight_music;
            GetComponent<AudioSource>().Play();
        }
    }

    public void SetDialogueMusic()
    {
        GetComponent<AudioSource>().clip = dialogue_music;
        GetComponent<AudioSource>().Play();
    }
}
