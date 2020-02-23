using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite BrokenSprite;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;

    // Start is called before the first frame update
    void Start ()
    {
        sr = GetComponent<SpriteRenderer> ();
    }

    public void Die ()
    {
        Instantiate (explosionPrefab, transform.position, transform.rotation);
        sr.sprite = BrokenSprite;
        PlayerManager.Instance.isDefeated = true;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}