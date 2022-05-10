using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip rocketSFX;
    AudioSource myAudioSource;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayRocketSoundEffect()
    {
        myAudioSource.PlayOneShot(rocketSFX);
    }

    public void StopRocketSoundEffect()
    {
        myAudioSource.Stop();
    }
}
