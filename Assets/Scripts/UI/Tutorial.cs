using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private int level;

    public void SetTutorial(int level)
    {
        this.level = level;
        Invoke("Click", 1.8f);
    }

    private void Click()
    {
        if (level == 1)
        {
            GetComponent<TMP_Text>().enabled = true;
            GetComponent<TMP_Text>().text = "Click!";
        }
        else
        {
            GetComponent<TMP_Text>().enabled = false;
        }
    }

    public void Clicked()
    {
        GetComponent<TMP_Text>().enabled = false;
    }

    public void Launched()
    {
        if (level == 1)
        {
            GetComponent<TMP_Text>().enabled = true;
            GetComponent<TMP_Text>().text = "Brush!";
        }
        else
        {
            GetComponent<TMP_Text>().enabled = false;
        }
    }

    public void Brushed()
    {
        GetComponent<TMP_Text>().enabled = false;
    }
}
