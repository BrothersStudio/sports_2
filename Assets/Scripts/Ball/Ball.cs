using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<Sprite> sprites;

    bool explosive = false;
    public GameObject explosion_prefab;

    public void SetLevel(int level)
    {
        if (level > 4)
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
        if (collision.gameObject.tag != "Ice" && collision.gameObject.tag != "Lines" && explosive)
        {
            Detonate(false);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            GetComponent<BallSounds>().HitWall();
        }
    }

    public void Detonate(bool end_of_level)
    {
        if (!end_of_level)
        {
            FindObjectOfType<FollowPlayer>().PauseCamera();
        }

        GameObject explosion = Instantiate(explosion_prefab, transform.position, transform.rotation);
        explosion.transform.Translate(new Vector3(-0.5f, 0, 0), Space.Self);

        gameObject.SetActive(false);
    }
}
