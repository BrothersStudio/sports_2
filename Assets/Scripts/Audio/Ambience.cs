using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour
{
    public AudioClip crowd_sound;

    public void SetAmbientSound(int level)
    {
        if (level == 1)
        {
            GetComponent<AudioSource>().clip = crowd_sound;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }
    }
}
