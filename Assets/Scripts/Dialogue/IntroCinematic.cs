using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCinematic : MonoBehaviour
{
    public bool show_intro;
    public GameObject text_1;
    public GameObject text_2;
    public GameObject text_3;

    private void Awake()
    {
        if (show_intro)
        {
            GetComponent<Image>().enabled = true;
            StartCoroutine(Cinematic());
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }

    private IEnumerator Cinematic()
    {
        text_1.SetActive(true);
        yield return new WaitForSeconds(1.2f);

        text_2.SetActive(true);
        yield return new WaitForSeconds(5f);

        text_3.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        text_1.GetComponent<FadeIn>().fade_out = true;
        text_2.GetComponent<FadeIn>().fade_out = true;
        text_3.GetComponent<FadeIn>().fade_out = true;

        yield return new WaitForSeconds(1);
        
        for (int i = 0; i < 100; i++)
        {
            Color bg_color = GetComponent<Image>().color;
            bg_color.a = Mathf.Clamp01(bg_color.a - 0.01f);
            GetComponent<Image>().color = bg_color;
            yield return null;
        }

        FindObjectOfType<FollowPlayer>().StartAnimationFromIntro();
        gameObject.SetActive(false);
    }
}
