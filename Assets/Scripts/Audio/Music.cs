using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private bool playing_dialogue_music = true;

    public AudioClip puzzle_music;
    public AudioClip dialogue_music;
    public AudioClip fight_music;

    public void SetLevelMusic(int level)
    {
        if (playing_dialogue_music)
        {
            playing_dialogue_music = false;

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
    }

    public void SetDialogueMusic()
    {
        playing_dialogue_music = true;

        GetComponent<AudioSource>().clip = dialogue_music;
        GetComponent<AudioSource>().Play();
    }

    public bool PlayingDialogueMusic()
    {
        return playing_dialogue_music;
    }
}
