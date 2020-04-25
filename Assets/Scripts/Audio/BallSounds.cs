using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSounds : MonoBehaviour
{
    new private Rigidbody2D rigidbody;
    private Launch launch;

    public AudioClip hit_wall_clip;
    public List<AudioClip> ice_clips;
    private AudioSource source;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        launch = GetComponent<Launch>();
        source = GetComponent<AudioSource>();
    }

    public void HitWall()
    {
        source.clip = hit_wall_clip;
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
    }

    private void Update()
    {
        if (rigidbody.velocity.magnitude > 0.5f || launch.launching)
        {
            if (!source.isPlaying)
            {
                source.clip = ice_clips[Random.Range(0, ice_clips.Count)];
                source.pitch = Random.Range(0.9f, 1.1f);
                source.Play();
            }
        }
    }
}
