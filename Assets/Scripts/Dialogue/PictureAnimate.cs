using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PictureAnimate : MonoBehaviour
{
    private TMP_Animated animatedText;

    private bool moved = false;
    private float move_amount = 10f;
    private Vector3 default_pos;

    void Awake()
    {
        default_pos = GetComponent<RectTransform>().localPosition;

        animatedText = transform.parent.parent.Find("Text Box/Text (TMP)").GetComponent<TMP_Animated>();
        animatedText.onTextReveal.AddListener((newChar) => AnimatePicture());
        animatedText.onDialogueFinish.AddListener(() => DoneCurrentText());
    }

    public void AnimatePicture()
    {
        if (moved)
        {
            GetComponent<RectTransform>().localPosition = default_pos;
            moved = false;
        }
        else
        {
            GetComponent<RectTransform>().localPosition += new Vector3(
                Random.Range(-move_amount, move_amount),
                Random.Range(-move_amount, move_amount),
                0);
            moved = true;
        }
    }

    private void DoneCurrentText()
    {
        GetComponent<RectTransform>().localPosition = default_pos;
        moved = false;
    }

    private void OnDisable()
    {
        GetComponent<RectTransform>().localPosition = default_pos;
        moved = false;
    }
}
