using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool rpg_mode = false;
    public List<Sprite> sprites;
    public List<Sprite> rpg_sprites;
    private GameObject clown_in_range = null;

    public void SetRPGMode()
    {
        rpg_mode = true;
    }

    private void Start()
    {
        if (rpg_mode)
        {
            StartCoroutine(AnimateRPG());
        }
        else
        {
            StartCoroutine(AnimateGrenade());
        }
    }

    private IEnumerator AnimateGrenade()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[i];
            if (i == 12)
            {
                GetComponent<AudioSource>().enabled = true;
                FindObjectOfType<FollowPlayer>().Shake(0.7f);
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

    private IEnumerator AnimateRPG()
    {
        for (int i = 0; i < rpg_sprites.Count; i++)
        {
            GetComponent<SpriteRenderer>().sprite = rpg_sprites[i];
            if (i == 0)
            {
                GetComponent<AudioSource>().enabled = true;
                FindObjectOfType<FollowPlayer>().Shake(0.9f);
            }
            else if (i == 1)
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
