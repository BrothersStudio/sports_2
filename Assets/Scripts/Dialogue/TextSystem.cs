using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSystem : MonoBehaviour
{
    [TextArea]
    public List<string> script;

    bool done_current_text = false;
    int ind_displaying = -1;
    public TMP_Animated text_box;
    public LevelLoader level_loader;

    public GameObject score;

    private void Awake()
    {
        text_box.onDialogueFinish.AddListener(() => DoneCurrentText());
    }

    public void Activate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        score.SetActive(false);

        ind_displaying = -1;
        DisplayNextText();
    }

    private void DoneCurrentText()
    {
        done_current_text = true;
    }

    private void Update()
    {
        if (done_current_text)
        {
            if (Input.GetMouseButton(0))
            {
                DisplayNextText();
            }
        }
    }

    public void DisplayNextText()
    {
        ind_displaying++;
        if (script.Count == ind_displaying)
        {
            // Done with script, next level
            Deactivate();
            level_loader.LoadNextLevel();
        }
        else
        {
            done_current_text = false;
            text_box.ReadText(script[ind_displaying]);
        }
    }

    public void Deactivate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        score.SetActive(true);
    }
}
