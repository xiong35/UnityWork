using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // attribute
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private bool isImmune = true;
    private float immuneTime = 3;
    // get some reference
    private SpriteRenderer sr;
    public Sprite[] tankSprite; // up r d l
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject immuneEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    private void Awake ()
    {
        sr = GetComponent<SpriteRenderer> ();
    }

    // Start is called before the first frame update
    void Start () { }

    // Update is called once per frame
    void Update ()
    {
        //immune?
        if (isImmune)
        {
            immuneEffectPrefab.SetActive (true);
            immuneTime -= Time.deltaTime;
            if (immuneTime <= 0)
            {
                isImmune = false;
                immuneEffectPrefab.SetActive (false);
            }
        }

        // attack cd
        if (timeVal >= 0.4f)
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
        if (PlayerManager.Instance.isDefeated)
        {
            return;
        }
        Move ();
    }
    // tank's attack method
    private void Attack ()
    {
        if (PlayerManager.Instance.isDefeated)
        {
            return;
        }
        if (Input.GetKeyDown (KeyCode.Space))
        {
            Instantiate (bulletPrefab, transform.position,
                // rotation needs to be transformed 
                // TODO: learn to understand it
                Quaternion.Euler (transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    // a method that controls the movement of entities
    private void Move ()
    {
        float horizontal = Input.GetAxisRaw ("Horizontal");
        transform.Translate (Vector3.right * horizontal *
            moveSpeed * Time.fixedDeltaTime, Space.World);
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

        if (horizontal != 0)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play ();
            }
            return;
        }

        float vertical = Input.GetAxisRaw ("Vertical");
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
        if (vertical != 0)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play ();
            }
            return;
        }
        moveAudio.clip = tankAudio[0];
        if (!moveAudio.isPlaying)
        {
            moveAudio.Play ();
        }
    }

    // trigger when the player get hit
    private void Hit ()
    {
        // immune
        if (isImmune)
        {
            return;
        }
        PlayerManager.Instance.isDead = true;
        // explode 
        Instantiate (explosionPrefab, transform.position, transform.rotation);
        // life -1
        Destroy (gameObject);
    }
}