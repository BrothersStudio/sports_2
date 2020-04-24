using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    public AudioClip hit_wall_clip;

    public void HitWall()
    {
        GetComponent<AudioSource>().clip = hit_wall_clip;
        GetComponent<AudioSource>().Play();
    }
}
