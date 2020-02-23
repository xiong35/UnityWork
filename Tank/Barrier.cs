using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public AudioClip hitAudio;

    public void PlayHitAudio()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
    }
}
