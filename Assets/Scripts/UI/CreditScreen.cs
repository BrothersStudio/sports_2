using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    public void Activate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        FindObjectOfType<Music>().Stop();
    }
}
