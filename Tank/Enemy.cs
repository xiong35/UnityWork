using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // attribute
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;

    private float vertical;
    private float horizontal;

    private float timeVal;
    private float timeValChangeDirection = 2;
    // get some reference
    private SpriteRenderer sr;
    public Sprite[] tankSprite; // up r d l
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private void Awake ()
    {
        sr = GetComponent<SpriteRenderer> ();
    }

    // Start is called before the first frame update
    void Start () { }

    // Update is called once per frame
    void Update ()
    {

        // attack cd
        if (timeVal >= 2.5f)
        {
            Attack ();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate ()
    {
        Move ();
    }
    // tank's attack method
    private void Attack ()
    {

        Instantiate (bulletPrefab, transform.position,
            // rotation needs to be transformed 
            // TODO: learn to understand it
            Quaternion.Euler (transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }

    // a method that controls the movement of entities
    private void Move ()
    {
        if (timeValChangeDirection >= 4f)
        {
            timeValChangeDirection = 0;
            int num = Random.Range (0, 8);
            if (num > 5)
            {
                vertical = -1;
                horizontal = 0;
            }
            else if (num == 0)
            {
                vertical = 1;
                horizontal = 0;
            }
            else if (num > 0 && num <= 2)
            {
                vertical = 0;
                horizontal = -1;
            }
            else if (num > 2 && num <= 4)
            {
                horizontal = 1;
                vertical = 0;
            }

        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        if (horizontal < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3 (0, 0, 90);
        }
        else if (horizontal > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3 (0, 0, -90);
        }
        transform.Translate (Vector3.right * horizontal *
            moveSpeed * Time.fixedDeltaTime, Space.World);

        if (horizontal != 0)
        {
            return;
        }
        transform.Translate (Vector3.up * vertical *
            moveSpeed * Time.fixedDeltaTime, Space.World);
        if (vertical < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3 (0, 0, -180);
        }
        else if (vertical > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3 (0, 0, 0);
        }
    }

    // trigger when the player get hit
    private void Hit ()
    {

        // explode 
        Instantiate (explosionPrefab, transform.position, transform.rotation);
        // life -1
        Destroy (gameObject);
        PlayerManager.Instance.playerScore++;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timeValChangeDirection = 4;
        }
    }
}