using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private int level_ind = 0;
    public List<GameObject> levels;
    private GameObject current_level = null;

    private void Awake()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        if (current_level != null)
        {
            Destroy(current_level);
        }

        current_level = Instantiate(levels[level_ind]);
        level_ind++;
    }
}
