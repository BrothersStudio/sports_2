using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<Sprite> sprites;

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
            }

            yield return new WaitForSeconds(0.0666666f);
        }

        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
}
