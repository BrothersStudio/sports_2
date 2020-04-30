using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<Sprite> sprites;
    private GameObject clown_in_range = null;

    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[i];
            if (i == 12)
            {
                GetComponent<AudioSource>().enabled = true;
                if (GetComponent<Renderer>().isVisible)
                {
                    FindObjectOfType<FollowPlayer>().Shake(0.25f);
                }
            }
            else if (i == 13)
            {
                if (clown_in_range != null)
                {
                    clown_in_range.GetComponent<Goal>().Explode();
                }
            }

            yield return new WaitForSeconds(0.0666666f);
        }

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Clown")
        {
            clown_in_range = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == clown_in_range)
        {
            clown_in_range = null;
        }
    }
}
