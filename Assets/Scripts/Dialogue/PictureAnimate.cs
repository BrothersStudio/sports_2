using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PictureAnimate : MonoBehaviour
{
    private TMP_Animated animatedText;

    private bool moved = false;
    private float move_amount = 20f;
    private Vector3 default_pos;

    void Start()
    {
        default_pos = transform.position;

        animatedText = transform.parent.Find("Text Box/Text (TMP)").GetComponent<TMP_Animated>();
        animatedText.onTextReveal.AddListener((newChar) => AnimatePicture());
    }

    public void AnimatePicture()
    {
        if (moved)
        {
            transform.position = default_pos;
            moved = false;
        }
        else
        {
            transform.position += new Vector3(
                Random.Range(0, 0),
                Random.Range(-move_amount, move_amount));
            moved = true;
        }
    }
}
