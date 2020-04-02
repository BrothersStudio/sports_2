using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBox : MonoBehaviour
{
    public bool on_me = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        on_me = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on_me = false;
    }
}
