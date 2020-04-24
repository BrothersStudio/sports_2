using TMPro;
using UnityEngine;

public class TextSounds : MonoBehaviour
{
    private TMP_Animated animatedText;

    public AudioClip[] voices;
    [Space]
    public AudioSource voiceSource;

    void Start()
    {
        animatedText = GetComponent<TMP_Animated>();
        animatedText.onTextReveal.AddListener((newChar) => ReproduceSound(newChar));
    }

    public void ReproduceSound(char c)
    {
        if (char.IsLetter(c) && !voiceSource.isPlaying)
        {
            voiceSource.Stop();
            voiceSource.loop = false;
            voiceSource.clip = voices[Random.Range(0, voices.Length)];
            voiceSource.Play();
        }
    }
}