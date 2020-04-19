using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public int debug_starting_level;

    private int level_ind;
    public List<GameObject> levels;
    private GameObject current_level = null;

    public FollowPlayer main_cam;
    public LevelNameDisplay level_name_display;

    public TrackScore score;
    public Scorecard scorecard;

    private void Awake()
    {
        level_ind = debug_starting_level - 1;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (current_level != null)
        {
            Destroy(current_level);
        }

        scorecard.gameObject.SetActive(false);

        current_level = Instantiate(levels[level_ind]);
        level_ind++;

        main_cam.MoveToGoal(current_level.transform.Find("Goal").position);
        level_name_display.TurnOn(current_level.GetComponent<Level>().level_name);

        score.RecordGoalPosition();
    }
}
