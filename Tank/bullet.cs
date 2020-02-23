using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float moveSpeed = 10;
    public bool fromPlayer;

    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate (transform.up * moveSpeed *
            Time.fixedDeltaTime, Space.World);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!fromPlayer)
                {
                    collision.SendMessage ("Hit");
                    Destroy (gameObject);
                }
                break;
            case "Heart":
                Destroy (gameObject);
                collision.SendMessage ("Die");
                break;
            case "Barrier":
                if (fromPlayer)
                {
                    collision.SendMessage ("PlayHitAudio");
                }
                Destroy (gameObject);
                break;
            case "Wall":
                Destroy (collision.gameObject);
                Destroy (gameObject);
                break;
            case "Enemy":
                if (fromPlayer)
                {
                    collision.SendMessage ("Hit");
                    Destroy (gameObject);
                }
                break;
            default:
                break;
        }
    }
}