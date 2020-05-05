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
    
    public TextSystem text_system;

    private void Awake()
    {
        level_ind = debug_starting_level - 1;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        if (level_ind == 8)
        {
            FindObjectOfType<CreditScreen>().Activate();
            return;
        }

        if (current_level != null)
        {
            current_level.GetComponentInChildren<BallSpawner>().CleanupBalls();
            FindObjectOfType<Field>().CleanupIce();
            Destroy(current_level);
        }

        current_level = Instantiate(levels[level_ind]);
        level_ind++;

        current_level.GetComponentInChildren<BallSpawner>().SetCurrentLevel(level_ind);
        level_name_display.TurnOn(current_level.GetComponent<Level>().level_name);
        GetComponent<LevelEndAnimation>().SetCurrentLevel(level_ind);
        text_system.SetNewLevel(level_ind - 1);
        FindObjectOfType<BallCounter>().SetCurrentLevel(level_ind);

        main_cam.SetCurrentLevel(level_ind);
        main_cam.StartNewLevelOverview();

        // Audio
        FindObjectOfType<Ambience>().SetAmbientSound(level_ind);
        FindObjectOfType<Music>().SetLevelMusic(level_ind);
    }
}
