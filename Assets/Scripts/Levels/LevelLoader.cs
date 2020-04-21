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
    public TextSystem text_system;

    private void Awake()
    {
        level_ind = debug_starting_level - 1;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (current_level != null)
        {
            current_level.GetComponentInChildren<BallSpawner>().CleanupBalls();
            Destroy(current_level);
        }

        text_system.Deactivate();

        current_level = Instantiate(levels[level_ind]);
        level_ind++;

        main_cam.MoveToGoal(current_level.GetComponentInChildren<Goal>().transform.position);
        level_name_display.TurnOn(current_level.GetComponent<Level>().level_name);

        score.RecordGoalPosition();
    }
}
