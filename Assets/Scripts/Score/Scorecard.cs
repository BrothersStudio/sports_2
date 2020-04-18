using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorecard : MonoBehaviour
{
    private int recorded_scores = 0;
    public List<GameObject> score_fields;

    private TrackScore track_score;

    private void Awake()
    {
        track_score = FindObjectOfType<TrackScore>();
    }

    private void OnEnable()
    {
        track_score.gameObject.SetActive(false);
        
        score_fields[recorded_scores].GetComponent<Text>().text = track_score.CalculateScore();
        score_fields[recorded_scores].SetActive(true);
        recorded_scores++;
    }

    private void OnDisable()
    {
        track_score.gameObject.SetActive(true);
    }
}
