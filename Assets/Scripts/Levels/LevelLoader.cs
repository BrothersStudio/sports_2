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

        // Intro cinematic moves camera on level 1
        if (level_ind != 1 || !FindObjectOfType<IntroCinematic>().show_intro)
        {
            main_cam.MoveToGoal(current_level.GetComponentInChildren<Goal>().transform.position);
        }

        current_level.GetComponentInChildren<BallSpawner>().SetCurrentLevel(level_ind);
        level_name_display.TurnOn(current_level.GetComponent<Level>().level_name);
        main_cam.SetCurrentLevel(level_ind);

        // Audio
        FindObjectOfType<Ambience>().SetAmbientSound(level_ind);
        FindObjectOfType<Music>().SetMusic(level_ind);

        score.RecordGoalPosition();
    }
}
