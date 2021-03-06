﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSystem : MonoBehaviour
{
    private int current_level;
    public List<ScriptHolder> scripts;

    bool done_current_text = false;
    int ind_displaying = -1;
    public TMP_Animated text_box;
    public LevelLoader level_loader;

    private void Awake()
    {
        text_box.onDialogueFinish.AddListener(() => DoneCurrentText());
    }

    public void SetNewLevel(int level_ind)
    {
        Deactivate();

        current_level = level_ind;
    }

    public void Activate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        FindObjectOfType<Ambience>().Stop();

        ind_displaying = -1;
        DisplayNextText();
    }

    private void DoneCurrentText()
    {
        done_current_text = true;
    }

    private void Update()
    {
        if (done_current_text && text_box.transform.parent.gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisplayNextText();
            }
        }
        else if (!done_current_text && text_box.transform.parent.gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                text_box.SkipText();
            }
        }
    }

    public void DisplayNextText()
    {
        ind_displaying++;
        if (scripts[current_level].script.Count <= ind_displaying)
        {
            // Done with script, next level
            Deactivate();
            level_loader.LoadNextLevel();
        }
        else
        {
            Music music = FindObjectOfType<Music>();
            if (!music.PlayingDialogueMusic())
            {
                music.SetDialogueMusic();
            }

            done_current_text = false;
            text_box.ReadText(scripts[current_level].script[ind_displaying]);
        }
    }

    public void Deactivate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
