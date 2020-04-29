using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool dead = false;
    
    public List<Sprite> body_part_sprites;
    public GameObject body_part;
    private List<GameObject> part_instances = new List<GameObject>();

    public AudioClip bump_sound;
    public List<AudioClip> death_scream;
    public List<AudioClip> death_crunch;
    public AudioSource source_1;
    public AudioSource source_2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !source_1.isPlaying)
        {
            source_1.clip = bump_sound;
            source_1.pitch = Random.Range(0.9f, 1.1f);
            source_1.Play();
        }
    }

    public void Explode()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;

        source_1.clip = death_scream[Random.Range(0, death_scream.Count)];
        source_1.Play();
        source_2.clip = death_crunch[Random.Range(0, death_crunch.Count)];
        source_2.Play();

        for (int i = 0; i < 10; i++)
        {
            GameObject part = Instantiate(body_part, transform.position, transform.rotation);
            part.GetComponent<SpriteRenderer>().sprite = body_part_sprites[Random.Range(0, body_part_sprites.Count)];
            part.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 100);
            part.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-4f, 4f));
            part_instances.Add(part);
        }

        transform.GetChild(0).gameObject.SetActive(true);
    }

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

    private void OnDestroy()
    {
        foreach (GameObject part in part_instances)
        {
            Destroy(part);
        }
    }
}
