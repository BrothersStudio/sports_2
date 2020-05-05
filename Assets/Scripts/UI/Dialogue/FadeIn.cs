using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public bool fade_out = false;

    private void FixedUpdate()
    {
        if (!fade_out)
        {
            Color text_color = GetComponent<Text>().color;
            text_color.a = Mathf.Clamp01(text_color.a + 0.02f);
            GetComponent<Text>().color = text_color;
        }
        else
        {
            Color text_color = GetComponent<Text>().color;
            text_color.a = Mathf.Clamp01(text_color.a - 0.01f);
            GetComponent<Text>().color = text_color;
        }
    }
}
