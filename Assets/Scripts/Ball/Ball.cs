using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<Sprite> sprites;

    bool explosive = false;
    public bool exploded = false;
    public GameObject explosion_prefab;

    public void SetLevel(int level)
    {
        if (level > 3)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
            explosive = true;
        }
        else if (level > 1)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject thing_hit = collision.gameObject;
        if (thing_hit.tag != "Ice" && thing_hit.tag != "Lines" && explosive)
        {
            Detonate(false);
            if (thing_hit.tag == "Clown")
            {
                thing_hit.GetComponent<Goal>().DisableAndCheckDone();
            }
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
            explosion.transform.Translate(new Vector3(0.13f, 0.28f, 0), Space.Self);

            gameObject.SetActive(false);
        }
    }
}
