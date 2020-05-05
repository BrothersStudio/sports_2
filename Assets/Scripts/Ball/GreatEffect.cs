using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatEffect : MonoBehaviour
{
    public List<Sprite> great_sprites;

    private void Awake()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        int i = 0;
        while (i < great_sprites.Count)
        {
            yield return new WaitForSeconds(0.0666666f);
            GetComponent<SpriteRenderer>().sprite = great_sprites[i];
            i++;
        }
        Destroy(gameObject);
    }
}
