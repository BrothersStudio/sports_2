using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScreen : MonoBehaviour
{
    public void Activate()
    {
        StartCoroutine(Animate());
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private IEnumerator Animate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.06666f);
            Color color = GetComponent<Image>().color;
            color.a += 0.01f;
        }
    }
}
