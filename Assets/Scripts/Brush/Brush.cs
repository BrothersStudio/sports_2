using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brush : MonoBehaviour
{
    private Rigidbody2D active_ball;
    private Camera main_camera;

    float last_brush = 0;
    float brush_cooldown = 0.02f;

    public bool brushing = false;
    private Vector2 last_mouse_pos;
    private bool left = false;

    public GameObject brush_particles;
    private SpriteRenderer sprite;

    public List<AudioClip> brush_sounds;
    public GameObject ice_prefab;

    private void Awake()
    {
        main_camera = Camera.main;
        sprite = GetComponent<SpriteRenderer>();

        Brushing(false);
    }

    public void RegisterNewBall(Transform new_ball)
    {
        active_ball = new_ball.GetComponent<Rigidbody2D>();
    }

    public void Brushing(bool brushing)
    {
        this.brushing = brushing;
        sprite.enabled = brushing;
    }

    private void Update()
    {
        Vector2 current_mouse_pos = main_camera.ScreenToWorldPoint(Input.mousePosition);
        if ((current_mouse_pos - last_mouse_pos).x < -0.1f)
        {
            left = true;
        }
        else if ((current_mouse_pos - last_mouse_pos).x > 0.1f)
        {
            left = false;
        }
        last_mouse_pos = current_mouse_pos;

        if (brushing)
        {
            Vector3 brush_pos = current_mouse_pos;
            brush_pos.x -= 0.7f;  // offset brush
            brush_pos.z = -9;
            transform.position = brush_pos;

            if (Input.GetMouseButton(0))// && Time.timeSinceLevelLoad > last_brush + brush_cooldown)
            {
                if (Physics2D.OverlapCircle(brush_pos + new Vector3(0.7f, 0), 0.05f, 1 << 11) == null)
                {
                    last_brush = Time.timeSinceLevelLoad;

                    brush_pos.z = 0;
                    GameObject ice = Instantiate(ice_prefab, brush_pos + new Vector3(0.7f, 0), Quaternion.identity);
                    ice.GetComponent<Ice>().SetBrush(this);
                    ice.GetComponent<Ice>().Brush();
                }
            }
        }
    }

    public void SpawnBrushParticles()
    {
        GameObject new_particles = Instantiate(brush_particles, main_camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        ParticleSystem.ShapeModule shape = new_particles.GetComponent<ParticleSystem>().shape;
        shape.rotation = new Vector3(0, left ? -90 : 90, 0);

        if (!GetComponent<AudioSource>().isPlaying)
        {
            AudioClip sound = brush_sounds[Random.Range(0, brush_sounds.Count)];

            GetComponent<AudioSource>().clip = sound;
            GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.1f);
            GetComponent<AudioSource>().Play();
        }
    }
}
