using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private int level_ind = 0;
    public List<GameObject> levels;
    private GameObject current_level = null;

    public FollowPlayer main_cam;
    public LevelNameDisplay level_name_display;

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

        main_cam.MoveToGoal(current_level.transform.Find("Goal").position);
        level_name_display.TurnOn(current_level.GetComponent<Level>().level_name);
    }
}
