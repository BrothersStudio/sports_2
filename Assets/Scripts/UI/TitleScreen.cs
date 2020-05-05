using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Sprite sprite_1;
    public Sprite sprite_2;

    private void Awake()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.6666666f);
            GetComponent<Image>().sprite = sprite_2;        
            yield return new WaitForSeconds(0.6666666f);
            GetComponent<Image>().sprite = sprite_1;
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
