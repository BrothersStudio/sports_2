using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<Sprite> sprites;
    
    private int rocket_sprite_ind = 0;
    public List<Sprite> rocket_sprites;
    private float trail_spawn_rate = 0.2f;
    public GameObject rocket_trail_prefab;
    public AudioClip rocket_clip;

    bool rpg_mode = false;
    bool explosive = false;
    public bool exploded = false;
    public GameObject explosion_prefab;

    public void SetLevel(int level)
    {
        if (level > 5)
        {
            // RPG Mode
            GetComponentInChildren<CalculateDrag>().SetRPGMode();
            GetComponent<Rigidbody2D>().angularDrag = 1000000;
            GetComponent<Rigidbody2D>().drag = 0;
            transform.rotation = Quaternion.identity;
            GetComponent<Launch>().launch_velocity = 20;

            // Audio
            StartCoroutine(RocketAudioStartDelay());

            // Animations
            GetComponent<SpriteRenderer>().sprite = rocket_sprites[0];
            StartCoroutine(AnimateRocket());
            StartCoroutine(SpawnTrails());

            rpg_mode = true;
            explosive = true;
        }
        else if (level > 1)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            explosive = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

    private IEnumerator RocketAudioStartDelay()
    {
        yield return new WaitForSeconds(1.5f);

        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().clip = rocket_clip;
        GetComponent<AudioSource>().volume = 0.5f;
        GetComponent<AudioSource>().Play();
        GetComponent<BallSounds>().enabled = false;
    }

    private IEnumerator AnimateRocket()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.0666666f);

            rocket_sprite_ind++;
            if (rocket_sprite_ind == rocket_sprites.Count)
            {
                rocket_sprite_ind = 0;
            }
            GetComponent<SpriteRenderer>().sprite = rocket_sprites[rocket_sprite_ind];
        }
    }

    private IEnumerator SpawnTrails()
    {
        while (true)
        {
            yield return new WaitForSeconds(trail_spawn_rate);

            Instantiate(rocket_trail_prefab, transform.position - new Vector3(0, 2), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject thing_hit = collision.gameObject;
        if (thing_hit.tag != "Ice" && thing_hit.tag != "Lines" && thing_hit.tag != "Body Part" && explosive)
        {
            Detonate(false);
        }
        else if (thing_hit.tag == "Wall" || thing_hit.tag == "Player")
        {
            GetComponent<BallSounds>().HitWall();
        }
    }

    public void Detonate(bool end_of_level)
    {
        if (!exploded)
        {
            exploded = true;

            if (!end_of_level)
            {
                FindObjectOfType<FollowPlayer>().PauseCamera();
            }

            GameObject explosion = Instantiate(explosion_prefab, transform.position, transform.rotation);
            if (rpg_mode)
            {
                explosion.GetComponent<Explosion>().SetRPGMode();
            }
            explosion.transform.Translate(new Vector3(0.13f, 0.28f, 0), Space.Self);

            gameObject.SetActive(false);
        }
    }
}
