using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            Application.Quit();
        }
    }
}
