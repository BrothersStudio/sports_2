using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTrail : MonoBehaviour
{
    public List<Sprite> rocket_trail_sprites;

    private void Awake()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        for (int i = 0; i < rocket_trail_sprites.Count; i++)
        {
            GetComponent<SpriteRenderer>().sprite = rocket_trail_sprites[i];

            yield return new WaitForSeconds(0.0666666f);
        }

        Destroy(gameObject);
    }
}
