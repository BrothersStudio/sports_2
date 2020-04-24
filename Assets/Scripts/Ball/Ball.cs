using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public List<Sprite> sprites;
    bool explosive = false;

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
            Debug.Log("Explosion!");
        }
        else if (collision.gameObject.tag == "Wall")
        {
            GetComponent<BallSounds>().HitWall();
        }
    }
}
