using UnityEngine;

public class RocketAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioClip rocketSFX;
    [SerializeField] AudioClip levelSuccessSFX;
    [SerializeField] AudioClip deathExplosionSFX;

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

    public float GetDeathExplosionAudioClipLength()
    {
        return deathExplosionSFX.length;
    }

    public void PlayDeathExplosionSoundEffect()
    {
        myAudioSource.PlayOneShot(deathExplosionSFX);
    }

    public float GetLevelSuccessAudioClipLength()
    {
        return levelSuccessSFX.length;
    }

    public void PlayLevelSuccessSoundEffect()
    {
        myAudioSource.PlayOneShot(levelSuccessSFX);
    }
}
