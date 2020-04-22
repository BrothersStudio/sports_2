using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelNameDisplay : MonoBehaviour
{
    public void TurnOn(string level_name)
    {
        GetComponent<TMP_Text>().text = level_name;
        gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
