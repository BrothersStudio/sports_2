using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckingBall : MonoBehaviour
{
    public bool dangerous = false;
    public Color dangerous_color;
    public Color safe_color;

    public void Fired()
    {
        dangerous = true;
        Invoke("Cooldown", 0.5f);

        GetComponent<SpriteRenderer>().color = dangerous_color;
    }

    void Cooldown()
    {
        dangerous = false;

        GetComponent<SpriteRenderer>().color = safe_color;
    }
}
