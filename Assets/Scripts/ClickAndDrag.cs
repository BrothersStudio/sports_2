using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    public GameObject drag_indicator;
    public int speed;

    private void OnMouseDown()
    {
        drag_indicator.SetActive(true);
    }

    private void OnMouseDrag()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(GetComponent<Rigidbody2D>().velocity, 1);

        Vector3[] positions = {Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position};
        positions[0].z = -1;
        drag_indicator.GetComponent<LineRenderer>().SetPositions(positions);
    }

    private void OnMouseUp()
    {
        drag_indicator.SetActive(false);
        Vector3[] positions = { new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
        drag_indicator.GetComponent<LineRenderer>().SetPositions(positions);

        GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)) * speed;
        GetComponent<WreckingBall>().Fired();
    }
}
